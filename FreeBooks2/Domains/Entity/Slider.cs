using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entity
{
    public class Slider
    {
        [ValidateNever]
        public Guid Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("SliderName"))]
        public string Name { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("SliderImage"))]
        public string? ImageName  { get; set; }
    }
}
