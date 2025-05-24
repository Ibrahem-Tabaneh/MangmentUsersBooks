using Domains.Entity;
using Domains.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            Users = new List<VwUser>();
            NewUser=new NewUser();
            Roles = new List<IdentityRole>();
            ChangePassword=new ChangePasswordViewMoodel();
        }
        public List<VwUser> Users { get; set; }
        public NewUser NewUser { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public ChangePasswordViewMoodel ChangePassword { get; set; }
    }

    public class NewUser
    {
        [ValidateNever]
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbUserName"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("MaxLength"))]
        [MinLength(3, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("MinLength"))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("RoleName"))]
        public string RoleName { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbEmail"))]
        [EmailAddress(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbValidEmail"))]
        public string Email { get; set; }
        [ValidateNever]
        public bool ActiveUser { get; set; }
        [ValidateNever]
        public string? ImageUser { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbPassword"))]
        [MinLength(6, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("MinLengthPassword"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("MaxLengthPassword"))]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbPassword"))]
        [Compare("Password", ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbPasswordCompare"))]

        public string ConfirmPassword { get; set; }
    }
}
