using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entity
{
    public class Category
    {
        [ValidateNever]
        public Guid Id { get; set; }
        [Required (ErrorMessageResourceType = typeof (Resources.ResourceData),ErrorMessageResourceName =("CategoryName"))]
        [MinLength (3,ErrorMessageResourceType =typeof(Resources.ResourceData),ErrorMessageResourceName =("MinLength"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MaxLength"))]
       
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CurrentStaut { get; set; }
    }
}
