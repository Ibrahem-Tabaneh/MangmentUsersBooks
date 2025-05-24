using Bl.IRepository;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoriesController : Controller
    {
        // #region Dependencies
        private readonly IServicesRepository<Category> _servicesCategory;
        private readonly IServicesRepositoryLog<LogCategory> _servicesCategoryLog;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(IServicesRepository<Category> servicesCategory,
                                    IServicesRepositoryLog<LogCategory> servicesCategoryLog,
                                    UserManager<ApplicationUser> userManager)
        {
            _servicesCategory = servicesCategory;
            _servicesCategoryLog = servicesCategoryLog;
            _userManager = userManager;
        }
        // #endregion

        // #region Actions
        [Authorize(Roles = "Admin,Customer,SuperAdmin")]
        public IActionResult Categories()
        {
            return View(new CategoryViewModel
            {
                Categories = _servicesCategory.GetAll(),
                LogCategories = _servicesCategoryLog.GetAll(),
                NewCategory = new Category()
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.NewCategory.Id == Guid.Empty)
                {
                    // Create new category
                    if (_servicesCategory.FindById(model.NewCategory.Name) != null)
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbMsgNameCategoryExists);
                    }
                    else
                    {
                        if (_servicesCategory.Save(model.NewCategory) &&
                            _servicesCategoryLog.Save(model.NewCategory.Id, Guid.Parse(userId)))
                        {
                            SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleSuccess, Resource.ResourceWeb.lbSuccessCreateCategory);
                        }
                        else
                        {
                            SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbErrorCreateCategory);
                        }
                    }
                }
                else
                {
                    // Update existing category
                    if (_servicesCategory.Save(model.NewCategory) &&
                        _servicesCategoryLog.Update(model.NewCategory.Id, Guid.Parse(userId)))
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleEdit, Resource.ResourceWeb.lbSuccessEditCategory);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbErrorEditCategory);
                    }
                }
            }
            return RedirectToAction("Categories");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            if (_servicesCategory.Delete(id) &&
                _servicesCategoryLog.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteCategory);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteCategory);
            }

            return RedirectToAction("Categories");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult DeleteLog(Guid id)
        {
            if (_servicesCategoryLog.DeleteLog(id))
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteCategoryLog);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteCategoryLog);
            }
            return RedirectToAction("Categories");
        }
        // #endregion

        private void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.msgType, msgType);
            HttpContext.Session.SetString(Helper.tittle, title);
            HttpContext.Session.SetString(Helper.msg, msg);
        }
    }
}
