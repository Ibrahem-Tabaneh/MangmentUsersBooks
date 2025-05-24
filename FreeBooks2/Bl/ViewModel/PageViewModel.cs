using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class PageViewModel
    {
        public PageViewModel()
        {
            
            page=new Page2();
            pages = new List<Page2>();
        }
        public Page2 page { get; set; }
        public List<Page2> pages { get; set; }
    }
}
