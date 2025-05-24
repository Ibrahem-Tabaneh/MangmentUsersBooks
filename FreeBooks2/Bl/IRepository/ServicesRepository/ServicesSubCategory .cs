using Bl.Data;
using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesSubSubCategory : IServicesRepository<SubCategory>
    {
        private readonly FreeBookDbContext _context;

        public ServicesSubSubCategory(FreeBookDbContext context)
        {
            _context = context;
        }
        public List<SubCategory> GetAll()
        {
            try
            {
                var model = _context.SubCategories.OrderBy(x => x.Name).Where(x => x.CurrentStaut == 1).ToList();
                return model;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SubCategory FindById(Guid Id)
        {
            try
            {
                return _context.SubCategories.FirstOrDefault(x => x.Id == Id && x.CurrentStaut==1);

            }
            catch (Exception ex)
            {
                return null;
            }
        }
      
        public bool Save(SubCategory model)
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
                    _context.SubCategories.Add(model);
                }
                else
                {
                    //update
                    result.Name = model.Name;
                    result.CategoryId=model.CategoryId;
                    result.CurrentStaut = (int)Helper.eCurrentStatu.Active;
                    _context.SubCategories.Update(result);
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
                var SubCategory=FindById(Id);
                SubCategory.CurrentStaut =(int)Helper.eCurrentStatu.Delete;
                _context.SubCategories.Update(SubCategory);
                _context.SaveChanges();

                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public SubCategory FindById(string Name)
        {
           return _context.SubCategories.FirstOrDefault(c => c.Name.Equals(Name)&&c.CurrentStaut==1);
        }
        public int Count()
        {
            return _context.SubCategories.Where(x => x.CurrentStaut == 1).Count();
        }
    }
}
