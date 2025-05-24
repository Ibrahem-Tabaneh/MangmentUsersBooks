using Domains.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Seed
{
    public class DefaultRole
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
          await  roleManager.CreateAsync( new IdentityRole(Helper.SuperAdmin));
          await roleManager.CreateAsync(new IdentityRole(Helper.Admin));
          await roleManager.CreateAsync(new IdentityRole(Helper.Customer));


        }
    }
}
