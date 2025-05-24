using Domains.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class ChangePasswordViewMoodel
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbPassword"))]
        [MinLength(6, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("MinLengthPassword"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("MaxLengthPassword"))]
        public string Password { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbPassword"))]
        [Compare("Password", ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("lbPasswordCompare"))]

        public string ConfirmPassword { get; set; }
    }
}
