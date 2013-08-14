using ClassesData;
using ClassesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebClient.Models;

namespace WebClient.Repositories
{
    public class UsersRepository : BaseRepository
    {
        private const string SessionKeyChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int SessionKeyLen = 50;

        private const string ValidUsernameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890";
        private const string ValidNicknameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinUsernameNicknameChars = 4;
        private const int MaxUsernameNicknameChars = 30;

        public static void CreateUser(string username, string authCode)
        {
            ValidateUsername(username);
            ValidateAuthCode(authCode);
            using (FoursquareContext context = new FoursquareContext())
            {
                var usernameToLower = username.ToLower();

                var dbUser = context.Users.FirstOrDefault(u => u.Username.ToLower() == usernameToLower);

                if (dbUser != null)
                {
                    if (dbUser.Username.ToLower() == usernameToLower)
                    {
                        throw new ServerErrorException("Username already exists", "ERR_DUP_USR");
                    }
                }

                dbUser = new User()
                {
                    Username = usernameToLower,
                    AuthCode = authCode
                };
                context.Users.Add(dbUser);
                context.SaveChanges();
            }

        }

        public static string LoginUser(string username, string authCode, out string nameToReturn)
        {
            ValidateUsername(username);
            ValidateAuthCode(authCode);
            var context = new FoursquareContext();
            using (context)
            {
                var usernameToLower = username.ToLower();
                var user = context.Users.FirstOrDefault(u => u.Username.ToLower() == usernameToLower && u.AuthCode == authCode);
                if (user == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }

                var sessionKey = GenerateSessionKey((int)user.Id);
                user.SessionKey = sessionKey;
                context.SaveChanges();
                nameToReturn = user.Username;
                return sessionKey;
            }
        }

        public static void LogoutUser(string sessionKey)
        {
            ValidateSessionKey(sessionKey);
            var context = new FoursquareContext();
            using (context)
            {
                var user = context.Users.FirstOrDefault(u => u.SessionKey == sessionKey);
                if (user == null)
                {
                    throw new ServerErrorException("Invalid user authentication", "INV_USR_AUTH");
                }
                user.SessionKey = null;
                context.SaveChanges();
            }
        }

        private static void ValidateUsername(string username)
        {
            if (username == null || username.Length < MinUsernameNicknameChars || username.Length > MaxUsernameNicknameChars)
            {
                throw new ServerErrorException("Username should be between 4 and 30 symbols long", "INV_USRNAME_LEN");
            }
            else if (username.Any(ch => !ValidUsernameChars.Contains(ch)))
            {
                throw new ServerErrorException("Username contains invalid characters", "INV_USRNAME_CHARS");
            }
        }

        private static void ValidateAuthCode(string authCode)
        {
            if (authCode.Length != Sha1CodeLength)
            {
                throw new ServerErrorException("Invalid authentication code length", "INV_USR_AUTH_LEN");
            }
        }

        private static string GenerateSessionKey(int userId)
        {
            StringBuilder keyChars = new StringBuilder(50);
            keyChars.Append(userId.ToString());
            while (keyChars.Length < SessionKeyLen)
            {
                int randomCharNum;
                lock (rand)
                {
                    randomCharNum = rand.Next(SessionKeyChars.Length);
                }
                char randomKeyChar = SessionKeyChars[randomCharNum];
                keyChars.Append(randomKeyChar);
            }
            string sessionKey = keyChars.ToString();
            return sessionKey;
        }

        private static void ValidateSessionKey(string sessionKey)
        {
            if (sessionKey.Length != SessionKeyLen || sessionKey.Any(ch => !SessionKeyChars.Contains(ch)))
            {
                throw new ServerErrorException("Invalid Password", "ERR_INV_AUTH");
            }
        }
    }
}