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
    public class HomeController : Controller
    {
        // #region Dependencies
        public UserManager<ApplicationUser> _UserManager { get; }
        public IServicesRepository<Book> _Servicesbook { get; }
        public IServicesRepository<Category> _Servicescategory { get; }
        public IServicesRepository<SubCategory> _Servicessubcategories { get; }

        public HomeController(UserManager<ApplicationUser> userManager,
            IServicesRepository<Book> servicesbook,
            IServicesRepository<Category> servicescategory,
            IServicesRepository<SubCategory> servicessubcategories)
        {
            _UserManager = userManager;
            _Servicesbook = servicesbook;
            _Servicescategory = servicescategory;
            _Servicessubcategories = servicessubcategories;
        }
        // #endregion

        // #region Actions
        [Authorize(Roles = "Admin,Customer,SuperAdmin")]
        public IActionResult Index()
        {
            AdminReportViewModel adminReport = new AdminReportViewModel
            {
                numUsers = _UserManager.Users.Count(),
                numBooks = _Servicesbook.Count(),
                numCategories = _Servicescategory.Count(),
                numSubCategories = _Servicessubcategories.Count()
            };
            return View(adminReport);
        }

        [AllowAnonymous]
        public IActionResult Denied()
        {
            return View();
        }
        // #endregion
    }
}
