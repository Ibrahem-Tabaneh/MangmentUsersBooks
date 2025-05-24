using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository
{
    public interface IServicesRepositorySlider <T> where T : class
    {
        public List<Slider> GetAll();
        public Slider GetById(Guid id);
        public bool Save (Slider slider);
        public bool Delete (Guid id);
    }
}
