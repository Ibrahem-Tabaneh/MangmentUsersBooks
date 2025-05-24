using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.ViewModel
{
    public class SliderViewModel
    {
        public SliderViewModel()
        {
            Sliders = new List<Slider>();
            NewSlider = new Slider();
        }
        public List<Slider> Sliders { get; set; }
        public Slider NewSlider { get; set; }
    }
}
