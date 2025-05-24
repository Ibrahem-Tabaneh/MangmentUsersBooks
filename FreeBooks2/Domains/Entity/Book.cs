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
    public class Book
    {
       
       [ValidateNever]
        public Guid Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("BookName"))]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MinLength"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MaxLength"))]

        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("AuthorName"))]
        [MinLength(3, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MinLength"))]
        [MaxLength(20, ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("MaxLength"))]
        public string Author { get; set; }
        public string? ImageName { get; set; }
        public string? ImageAuthor { get; set; }
        public string? FileName  { get; set; }
        public string?  Description { get; set; }
        public bool Publish { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName =("SelectCategory"))]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.ResourceData), ErrorMessageResourceName = ("SelectSubCategory"))]
        public Guid SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        [ValidateNever]
        public SubCategory SubCategory { get; set; }

        public int CurrentStaut { get; set; }


    }
}
