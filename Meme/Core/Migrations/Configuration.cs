using Common.Constants;
using Common.Enums;
using Core.Model.Entities;

namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Core.Model.MemeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.MemeContext context)
        {
            // Insert admin user
            if (!context.User.Any(x => x.UserName == AppConstants.AdminUsername))
            {
                context.User.Add(new User
                {
                    UserName = AppConstants.AdminUsername,
                    Password = AppConstants.AdminPassword,
                    SaltKey = AppConstants.AdminSaltKey,
                    FirstName = "Phuong",
                    LastName = "Tran",
                    Role = (int)UserRoleEnums.Administrator,
                    IsActive = true,
                    IsDelete = false,
                    CreatedDate = DateTime.Now
                });
            }
        }
    }
}
