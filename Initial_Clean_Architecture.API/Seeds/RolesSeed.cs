using Initial_Clean_Architecture.Application.Domain.SeedingData;
using Initial_Clean_Architecture.Data.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Initial_Clean_Architecture.API.Seeds
{
    public static class RolesSeed
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            var props = typeof(RolesData).GetFields();

            foreach (var prop in props)
            {
                var roleName = prop.GetValue(typeof(RolesData)).ToString();

                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    var role = new IdentityRole
                    {
                        Name = roleName
                    };

                    roleManager.CreateAsync(role).Wait();
                }

            }
        }
    }
}
