using Bl.Data;
using Domains.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesCategoryLog : IServicesRepositoryLog<LogCategory>
    {
        private readonly FreeBookDbContext _context;

        public ServicesCategoryLog(FreeBookDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logcategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Delete,
                    CategoryId = Id,
                    Date = DateTime.Now,
                };
                _context.LogCategories.Add(logcategory);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public bool DeleteLog(Guid Id)
        {
           var log=FindById(Id);
            if (log != null)
            {
                _context.LogCategories.Remove(log);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public LogCategory FindById(Guid Id)
        {
            return _context.LogCategories.FirstOrDefault(x=>x.Id==Id);
        }

        public List<LogCategory> GetAll()
        {
           return _context.LogCategories.Include(x=>x.Category).OrderByDescending(x=>x.Date).ToList();
        }

        public   bool Save(Guid Id, Guid UserId)
        {
            LogCategory logcategory;
            try
            {
                 logcategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Create,
                    CategoryId = Id,
                    Date = DateTime.Now
                };


                    _context.LogCategories.Add(logcategory);
                    _context.SaveChanges();
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Guid Id, Guid UserId)
        {
            try
            {
                var logcategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Update,
                    CategoryId = Id,
                    Date = DateTime.Now,
                };
                _context.LogCategories.Add(logcategory);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
