using Initial_Clean_Architecture.Application.Domain.SeedingData;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Seeds
{
    public static class UsersSeed
    {
        public static void Seed(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            var superAdminSettings = new SuperAdminSettings();
            configuration.GetSection(superAdminSettings.GetType().Name).Bind(superAdminSettings);

            string email = superAdminSettings.Email;

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = superAdminSettings.FirstName,
                    FamilyName = superAdminSettings.FamilyName,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                };
                //ensure that there is only 1 super admin
                var superAdmins = userManager.GetUsersInRoleAsync(RolesData.SuperAdmin).Result;
                if (superAdmins.Count > 0)
                    return;
                //this password should reseted after publish
                var result = userManager.CreateAsync(user, "Aa123456").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RolesData.SuperAdmin).Wait();
                }
            }
        }
    }
}
