using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class BookViewModel
    {
        public BookViewModel()
        {
            Books = new List<Book>();
            LogBooks = new List<LogBook>();
            NewBook = new Book();
            SubCategories = new List<SubCategory>();
            Categories = new List<Category>();  
        }
        public List<Book> Books { get; set; }
        public List<LogBook> LogBooks { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Category> Categories { get; set; }
        public Book NewBook { get; set; }
    }
}
