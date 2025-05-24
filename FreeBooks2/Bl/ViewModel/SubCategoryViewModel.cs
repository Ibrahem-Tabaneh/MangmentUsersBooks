using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class SubCategoryViewModel
    {
        public SubCategoryViewModel()
        {
            SubCategories = new List<SubCategory>();
            LogSubCategories = new List<LogSubCategory>();
            NewSubCategory = new SubCategory();
            Categories = new List<Category>();  
        }
        public List<SubCategory> SubCategories { get; set; }
        public List<LogSubCategory> LogSubCategories { get; set; }
        public SubCategory NewSubCategory { get; set; }
        public  List<Category> Categories { get; set; }
    }
}
