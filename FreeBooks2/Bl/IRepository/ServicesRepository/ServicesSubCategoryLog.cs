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
    public class ServicesSubCategoryLog : IServicesRepositoryLog<LogSubCategory>
    {
        private readonly FreeBookDbContext _context;

        public ServicesSubCategoryLog(FreeBookDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var LogSubCategory = new LogSubCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Delete,
                    SubCategoryId=Id,
                    Date = DateTime.Now,
                };
                _context.LogSubCategories.Add(LogSubCategory);
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
                _context.LogSubCategories.Remove(log);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public LogSubCategory FindById(Guid Id)
        {
            return _context.LogSubCategories.FirstOrDefault(x=>x.Id==Id);
        }

        public List<LogSubCategory> GetAll()
        {
           return _context.LogSubCategories.Include(x=>x.SubCategory).OrderByDescending(x=>x.Date).ToList();
        }

        public   bool Save(Guid Id, Guid UserId)
        {
            LogSubCategory LogSubCategory;
            try
            {
                 LogSubCategory = new LogSubCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Create,
                    SubCategoryId = Id,
                    Date = DateTime.Now
                };


                    _context.LogSubCategories.Add(LogSubCategory);
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
                var LogSubCategory = new LogSubCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Update,
                    SubCategoryId = Id,
                    Date = DateTime.Now,
                };
                _context.LogSubCategories.Add(LogSubCategory);
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
