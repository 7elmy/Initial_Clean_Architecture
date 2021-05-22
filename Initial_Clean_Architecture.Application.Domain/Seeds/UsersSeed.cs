using Initial_Clean_Architecture.Application.Domain.Constants;
using Initial_Clean_Architecture.Application.Domain.Settings;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.Application.Domain.Seeds
{
    public static class UsersSeed
    {
        public static void Seed(UserManager<AppUser> userManager, SuperAdminSettings superAdminSettings)
        {

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
                    Role = RolesConst.SuperAdmin
                };
                //ensure that there is only 1 super admin
                var superAdmins = userManager.GetUsersInRoleAsync(RolesConst.SuperAdmin).Result;
                if (superAdmins.Count > 0)
                    return;
                //this password should be reset after publish
                var result = userManager.CreateAsync(user, Guid.NewGuid().ToString()).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, RolesConst.SuperAdmin).Wait();
                }
            }
        }
    }
}
