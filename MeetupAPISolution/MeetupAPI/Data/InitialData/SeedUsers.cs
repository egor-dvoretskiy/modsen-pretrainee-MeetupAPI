using MeetupAPI.Models.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data.InitialData
{
    public static class SeedUsers
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using var context = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>());
            
            if (context == null || context.UserModels == null)
            {
                //throw new ArgumentNullException($"{nameof(context)} is null at SeedData.");

                return;
            }

            if (context.UserModels.Any())
            {
                return;
            }

            context.UserModels.Add(GetSuperUser());

            context.SaveChanges();
            
        }

        private static UserModel GetSuperUser()
        {
            return new UserModel
            {
                UserName = "admin",
                Password = "admin"
            };
        }

    }
}
