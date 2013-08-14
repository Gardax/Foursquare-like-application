using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebClient.Models;
using WebClient.Repositories;

namespace WebClient.Controllers
{
    public class UserController : BaseApiController
    {
        [HttpPost]
        [ActionName("register")]
        public HttpResponseMessage RegisterUser([FromBody]UserModel user)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                UsersRepository.CreateUser(user.Username, user.AuthCode);
                string username = string.Empty;
                var sessionKey = UsersRepository.LoginUser(user.Username, user.AuthCode, out username);
                return new UserLoggedModel()
                {

                    SessionKey = sessionKey
                };
            });
            return responseMsg;
        }

        [HttpPost]
        [ActionName("login")]
        public HttpResponseMessage LoginUser([FromBody]UserModel user)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                string username = string.Empty;
                var sessionKey = UsersRepository.LoginUser(user.Username, user.AuthCode, out username);
                return new UserLoggedModel()
                {
                    SessionKey = sessionKey
                };
            });
            return responseMsg;
        }

        [HttpGet]
        [ActionName("logout")]
        public HttpResponseMessage LogoutUser(string sessionKey)
        {
            var responseMsg = this.PerformOperation(() =>
            {
                UsersRepository.LogoutUser(sessionKey);
            });
            return responseMsg;
        }
    }
}