using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class ViewModelHomePage
    {
        public ViewModelHomePage()
        {
            sliders = new List<Slider>();
            Setting = new Setting();
            page = new Page2();
            books = new List<Book>();
        }
        public List<Slider> sliders;
        public Setting Setting { get; set; }
        public Page2 page;
       public List<Book> books;
    }
}
