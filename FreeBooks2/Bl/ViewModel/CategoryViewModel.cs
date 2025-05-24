using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Categories=new List<Category>();
            LogCategories = new List<LogCategory>();
            NewCategory = new Category();   
        }
        public List<Category> Categories { get; set; }
        public List<LogCategory> LogCategories { get; set; }
        public Category NewCategory { get; set; }
    }
}
