using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser() { }

        public string Name { get; set; }
        public string? ImageName { get; set; }
        public bool ActiveUser { get; set; }
    }
}
