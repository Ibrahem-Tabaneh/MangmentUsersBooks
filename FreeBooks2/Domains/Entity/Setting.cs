using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entity
{
    public class Setting
    {
        public Setting()
        {
            
        }
        [ValidateNever]
        public Guid Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("websiteName"))]
        public string websiteName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("numberContact"))]
        [RegularExpression(@"^\d{13}$", ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = "InvalidContactNumber")]
        public string numberContact { get; set; }
        [ValidateNever]
        public string? logo { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("facebookLink"))]
        [Url(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("UrlfacebookLink"))]
        public string facebookLink { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("instagramLink"))]
        [Url(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("UrlinstagramLink"))]
        public string instagramLink { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("linkedinLink"))]
        [Url(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("UrllinkedinLink"))]
        public string linkedinLink { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("twitterLink"))]
        [Url(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("UrltwitterLink"))]
        public string twitterLink { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("Email"))]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("ValidEmail"))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("Location"))]
        [Url(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("UrlLocationLink"))]
        public string Location { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("Address"))]
		public string Address {  get; set; }

    }
}
