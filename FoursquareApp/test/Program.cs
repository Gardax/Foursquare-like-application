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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FoursquareContext, ClassesData.Migrations.Configuration>());
            FoursquareContext context = new FoursquareContext();
            User user = new User();
            user.Username = "user1";
            user.SessionKey = "ses";
            user.Latitude = 1;
            user.Longitude = 2;

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
