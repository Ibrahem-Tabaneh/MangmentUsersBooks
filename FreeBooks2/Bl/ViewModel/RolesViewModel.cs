using Domains.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class RolesViewModel
    {
        public RolesViewModel()
        {
            Roles = new List<IdentityRole>();
            NewRole = new NewRole();
        }
        public  List<IdentityRole> Roles { get; set; }
        public NewRole NewRole { get; set; }
    }
    public class NewRole
    {
        [ValidateNever]
        public string RoleId { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResourceData),ErrorMessageResourceName =("RoleName"))]
        public string Name { get; set; }
    }
}
