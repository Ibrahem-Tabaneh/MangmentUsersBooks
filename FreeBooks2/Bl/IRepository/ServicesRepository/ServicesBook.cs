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
    public class ServicesBook : IServicesRepository<Book>
    {
        private readonly FreeBookDbContext _context;

        public ServicesBook(FreeBookDbContext context)
        {
            _context = context;
        }
        public List<Book> GetAll()
        {
            try
            {
                return _context.Books.Include(x=>x.Category)
                    .Include(z=>z.SubCategory).OrderBy(x => x.Name)
                    .Where(x=>x.CurrentStaut==1).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Book FindById(Guid Id)
        {
            try
            {
                return _context.Books.FirstOrDefault(x => x.Id == Id && x.CurrentStaut==1);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        public bool Save(Book model)
        {
            try
            {
                var result=FindById(model.Id);
                if (result == null)
                {
                    //create
                    var name=FindById(model.Name);
                    

                    if (name != null)
                        return false;
                    model.Id = Guid.NewGuid();
                    model.CurrentStaut =(int)Helper.eCurrentStatu.Active;
                    _context.Books.Add(model);
                }
                else
                {
                    //update
                    result.Name = model.Name;
                    result.Author = model.Author;
                    result.Publish=model.Publish;
                    result.ImageName = model.ImageName;
                    result.ImageAuthor = model.ImageAuthor;
                    result.FileName = model.FileName;
                    result.CategoryId = model.CategoryId;
                    result.SubCategoryId = model.SubCategoryId;
                    result.Description = model.Description;
                    result.CurrentStaut = (int)Helper.eCurrentStatu.Active;
                    _context.Books.Update(result);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(Guid Id)
        {
            try
            {
                var Book=FindById(Id);
                Book.CurrentStaut =(int)Helper.eCurrentStatu.Delete;
                _context.Books.Update(Book);
                _context.SaveChanges();

                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public Book FindById(string Name)
        {
           return _context.Books.FirstOrDefault(c => c.Name.Equals(Name)&&c.CurrentStaut==1);
        }

        public int Count()
        {
            return _context.Books.Where(x=>x.CurrentStaut==1).Count();
        }
    }
}
