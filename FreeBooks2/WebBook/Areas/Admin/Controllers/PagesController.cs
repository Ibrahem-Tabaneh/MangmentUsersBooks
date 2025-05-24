using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class PagesController : Controller
    {
        // #region Dependencies
        private readonly IServicesRepositoryPage<Page2> _servicesPage;

        public PagesController(IServicesRepositoryPage<Page2> servicesPage)
        {
            _servicesPage = servicesPage;
        }
        // #endregion

        // #region Actions
        public IActionResult Pages()
        {
            PageViewModel pageViewModel = new PageViewModel
            {
                pages = _servicesPage.GetAll(),
                page = new Page2()
            };

            return View(pageViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Page2 page, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var oldPage = _servicesPage.GetById(page.Id);

                if (files.Count == 0 || files == null)
                {
                    page.ImageName = oldPage.ImageName; // لا تغيير على الشعار
                }
                else
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/Page/" + oldPage.ImageName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    page.ImageName = await Helper.UploadImage(files, "Page");
                }

                if (_servicesPage.Save(page))
                {
                    SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleEdit, Resource.ResourceWeb.lbPgeEditSuccess);
                }
                else
                {
                    SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbPgeEditError);
                }
            }
            return RedirectToAction("Pages");
        }
        // #endregion

        // #region Private Methods
        private void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.msgType, msgType);
            HttpContext.Session.SetString(Helper.tittle, title);
            HttpContext.Session.SetString(Helper.msg, msg);
        }
        // #endregion
    }
}
