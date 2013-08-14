using ClassesData;
using ClassesModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main()
        {
            FoursquareContext context = new FoursquareContext();
            User user = new User();
            user.Username = "user1";
            user.SessionKey = "ses";

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
