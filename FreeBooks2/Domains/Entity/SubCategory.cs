using Domains.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Entity
{
    public class SubCategory
    {
        
        [ValidateNever]
        public Guid Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("SubCategoryName"))]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MinLength"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MaxLength"))]

        public string Name { get; set; }
        public int CurrentStaut { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = ("SelectCategoryName"))]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
