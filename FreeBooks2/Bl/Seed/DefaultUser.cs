using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Seed
{
    public class DefaultUser
    {
        public static async Task SeedDefaultSuperAdmin(UserManager<ApplicationUser> userManager)
        {
            var DefaultUser = new ApplicationUser
            {
                Name=Helper.Name,
                Email=Helper.Email,
                UserName= Helper.UserName,
                ActiveUser=true,
                EmailConfirmed=true,
                ImageName=Helper.ImageName

            };

            var user= userManager.FindByEmailAsync(DefaultUser.Email);
            if (user.Result==null)
            {
                await userManager.CreateAsync(DefaultUser,Helper.Password);
                await userManager.AddToRoleAsync(DefaultUser, Helper.SuperAdmin);
            }
        }
        public static async Task SeedDefaultCustomer(UserManager<ApplicationUser> userManager)
        {
            var DefaultUser = new ApplicationUser
            {
                Name = Helper.CustomerName,
                Email = Helper.CustomerEmail,
                UserName = Helper.CustomerUserName,
                ActiveUser = true,
                EmailConfirmed = true,
                ImageName = Helper.ImageName

            };

            var user = userManager.FindByEmailAsync(DefaultUser.Email);
            if (user.Result == null)
            {
                await userManager.CreateAsync(DefaultUser, Helper.CustomerPassword);
                await userManager.AddToRoleAsync(DefaultUser, Helper.Customer);
            }
        }

    }
}
