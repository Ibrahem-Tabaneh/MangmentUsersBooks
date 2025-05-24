using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository
{
    public interface IServicesRepository<T> where T : class
    {
        public List<T> GetAll();
        public T FindById(Guid Id);
       public T FindById(string Name);
       public bool Save(T model);
       public bool Delete(Guid Id);
       public int Count();
    }
}
