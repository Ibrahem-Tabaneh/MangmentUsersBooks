using Bl.Data;
using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesSlider : IServicesRepositorySlider<Slider>
    {
        private readonly FreeBookDbContext _context;

        public ServicesSlider(FreeBookDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid id)
        {
            try
            {
                var slider=GetById(id);
                _context.Sliders.Remove(slider);
                _context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public List<Slider> GetAll()
        {
            try
            {
                return _context.Sliders.ToList();

            }catch (Exception ex)
            {
                return null;
            }
        }

        public Slider GetById(Guid id)
        {
            try
            {
                return _context.Sliders.FirstOrDefault(x=>x.Id==id);
            }catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Slider slider)
        {
            try
            {
                var result = GetById(slider.Id);

                if (result==null)
                    {
                    //create
                    slider.Id=Guid.NewGuid();
                    _context.Sliders.Add(slider);
                    }

                else { 
                    //update
                    result.Name = slider.Name;
                    result.ImageName= slider.ImageName;
                    _context.Sliders.Update(result);
                     }

                _context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
