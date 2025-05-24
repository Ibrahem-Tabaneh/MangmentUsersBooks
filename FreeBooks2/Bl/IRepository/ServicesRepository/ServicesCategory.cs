using Bl.Data;
using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesCategory : IServicesRepository<Category>
    {
        private readonly FreeBookDbContext _context;

        public ServicesCategory(FreeBookDbContext context)
        {
            _context = context;
        }
        public List<Category> GetAll()
        {
            try
            {
                return _context.Categories.OrderBy(x => x.Name).Where(x=>x.CurrentStaut==1).ToList();

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Category FindById(Guid Id)
        {
            try
            {
                return _context.Categories.FirstOrDefault(x => x.Id == Id && x.CurrentStaut==1);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        public bool Save(Category model)
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
                    _context.Categories.Add(model);
                }
                else
                {
                    //update
                    result.Name = model.Name;
                    result.Description = model.Description;
                    result.CurrentStaut = (int)Helper.eCurrentStatu.Active;
                    _context.Categories.Update(result);
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
                var category=FindById(Id);
                category.CurrentStaut =(int)Helper.eCurrentStatu.Delete;
                _context.Categories.Update(category);
                _context.SaveChanges();

                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public Category FindById(string Name)
        {
           return _context.Categories.FirstOrDefault(c => c.Name.Equals(Name)&&c.CurrentStaut==1);
        }

        public int Count()
        {
            return _context.Categories.Where(x=>x.CurrentStaut==1).Count();
        }
    }
}
