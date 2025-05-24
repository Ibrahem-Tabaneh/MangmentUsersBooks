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
    public class ServicesBookLog : IServicesRepositoryLog<LogBook>
    {
        private readonly FreeBookDbContext _context;

        public ServicesBookLog(FreeBookDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var LogBook = new LogBook
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Delete,
                    BookId = Id,
                    Date = DateTime.Now,
                };
                _context.LogBooks.Add(LogBook);
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
                _context.LogBooks.Remove(log);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public LogBook FindById(Guid Id)
        {
            return _context.LogBooks.FirstOrDefault(x=>x.Id==Id);
        }

        public List<LogBook> GetAll()
        {
           return _context.LogBooks.Include(x=>x.Book).ThenInclude
                (z => z.SubCategory)
                .Include(a=>a.Book).ThenInclude(c=>c.Category)
                .OrderByDescending(x=>x.Date).ToList();
        }

        public   bool Save(Guid Id, Guid UserId)
        {
            LogBook LogBook;
            try
            {
                 LogBook = new LogBook
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Create,
                    BookId = Id,
                    Date = DateTime.Now
                };


                    _context.LogBooks.Add(LogBook);
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
                var LogBook = new LogBook
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Action = Helper.Update,
                    BookId = Id,
                    Date = DateTime.Now,
                };
                _context.LogBooks.Add(LogBook);
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
