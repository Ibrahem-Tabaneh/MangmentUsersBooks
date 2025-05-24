using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository
{
    public interface IServicesRepositoryLog<T> where T : class
    {
       public List<T> GetAll();
       public T FindById(Guid Id);
       public bool Save(Guid Id,Guid UserId);
       public bool Update(Guid Id, Guid UserId);
       public bool Delete(Guid Id, Guid UserId);
       public bool DeleteLog(Guid Id);    

    }
}
