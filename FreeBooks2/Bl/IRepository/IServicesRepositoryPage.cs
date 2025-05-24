using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository
{
    public interface IServicesRepositoryPage <T> where T : class
    {
        public List<T> GetAll();
        public T GetById(Guid Id);
        public bool Save (T page);
        
    }
}
