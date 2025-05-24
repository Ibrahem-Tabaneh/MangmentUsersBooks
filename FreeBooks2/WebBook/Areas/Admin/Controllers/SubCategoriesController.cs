using Bl.IRepository;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubCategoriesController : Controller
    {
        // #region Dependencies
        private readonly IServicesRepository<SubCategory> _servicesSubCategory;
        private readonly IServicesRepositoryLog<LogSubCategory> _servicesSubCategoryLog;
        private readonly IServicesRepository<Category> _servicesCategory;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubCategoriesController(IServicesRepository<SubCategory> servicesSubCategory,
                                       IServicesRepositoryLog<LogSubCategory> servicesSubCategoryLog,
                                       UserManager<ApplicationUser> userManager,
                                       IServicesRepository<Category> servicesCategory)
        {
            _servicesSubCategory = servicesSubCategory;
            _servicesSubCategoryLog = servicesSubCategoryLog;
            _servicesCategory = servicesCategory;
            _userManager = userManager;
        }
        // #endregion

        // #region Actions
        [Authorize(Roles = "Admin,Customer,SuperAdmin")]
        public IActionResult SubCategories()
        {
            return View(new SubCategoryViewModel
            {
                SubCategories = _servicesSubCategory.GetAll(),
                LogSubCategories = _servicesSubCategoryLog.GetAll(),
                NewSubCategory = new SubCategory(),
                Categories = _servicesCategory.GetAll()
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(SubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.NewSubCategory.Id == Guid.Empty)
                {
                    // Create
                    if (_servicesSubCategory.FindById(model.NewSubCategory.Name) != null)
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbMsgNameSubCategoryExists);
                    }
                    else
                    {
                        if (_servicesSubCategory.Save(model.NewSubCategory) &&
                            _servicesSubCategoryLog.Save(model.NewSubCategory.Id, Guid.Parse(userId)))
                        {
                            SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleSuccess, Resource.ResourceWeb.lbSuccessCreateSubCategory);
                        }
                        else
                        {
                            SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbErrorCreateSubCategory);
                        }
                    }
                }
                else
                {
                    // Update
                    if (_servicesSubCategory.Save(model.NewSubCategory) &&
                        _servicesSubCategoryLog.Update(model.NewSubCategory.Id, Guid.Parse(userId)))
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleEdit, Resource.ResourceWeb.lbSuccessEditCategory);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbErrorEditCategory);
                    }
                }
            }
            return RedirectToAction("SubCategories");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            if (_servicesSubCategory.Delete(id) && _servicesSubCategoryLog.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteSubCategory);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteSubCategory);
            }

            return RedirectToAction("SubCategories");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult DeleteLog(Guid id)
        {
            if (_servicesSubCategoryLog.DeleteLog(id))
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteCategoryLog);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteCategoryLog);
            }

            return RedirectToAction("SubCategories");
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
