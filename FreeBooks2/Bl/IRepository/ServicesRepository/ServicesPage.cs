using Bl.Data;
using Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.IRepository.ServicesRepository
{
    public class ServicesPage : IServicesRepositoryPage<Page2>
    {
        private readonly FreeBookDbContext _context;

        public ServicesPage(FreeBookDbContext context)
        {
            _context = context;
        }

		public List<Page2> GetAll()
		{
			try
			{
				var pages = _context.Pages.ToList();

                return pages;

			}catch (Exception ex)
			{
				return new List<Page2>();
			}
		}

		public Page2 GetById(Guid Id)
		{
			try
			{
				var page = _context.Pages.FirstOrDefault(x => x.Id == Id);
				return page;

			}catch (Exception ex)
			{
				return new Page2();
			}
		}

		public bool Save(Page2 page)
		{
			try
			{
				var oldpage= GetById(page.Id);
				oldpage.Desc = page.Desc;
				oldpage.Tittle = page.Tittle;
				oldpage.ImageName = page.ImageName;
				_context.Pages.Update(oldpage);
				_context.SaveChanges();
				return true;

			} catch (Exception ex)
			{
				return false;
			}
		}
	}
}
