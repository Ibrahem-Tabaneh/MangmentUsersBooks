using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository
{
    public interface IServicesRepositorySetting <T> where T : class
    {
        public Setting GetAll();
        public bool Save (Setting setting);
        
    }
}
