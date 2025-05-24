using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebBook.Models;

namespace WebBook.Controllers
{
    public class HomeController : Controller
    {
        // #region Dependencies
        private readonly IServicesRepositorySlider<Slider> _servicesSlider;
        public IServicesRepositorySetting<Setting> _ServicesSetting { get; }
        public IServicesRepositoryPage<Page2> _servicesPage { get; }
        public IServicesRepository<Book> _servicesBook;

        public HomeController(IServicesRepositorySlider<Slider> servicesSlider,
            IServicesRepositorySetting<Setting> servicesSetting,
            IServicesRepositoryPage<Page2> servicesPage,
            IServicesRepository<Book> servicesBook)
        {
            this._servicesSlider = servicesSlider;
            this._ServicesSetting = servicesSetting;
            this._servicesPage = servicesPage;
            this._servicesBook = servicesBook;
        }
        // #endregion

        // #region Actions
        public IActionResult Index()
        {
            ViewModelHomePage viewModelHomePage = new ViewModelHomePage
            {
                sliders = _servicesSlider.GetAll(),
                Setting = _ServicesSetting.GetAll(),
                books = _servicesBook.GetAll().Take(3).ToList(),
                page = new Page2()
            };

            return View(viewModelHomePage);
        }

        public IActionResult Page(int id)
        {
            var viewModelHomePage = new ViewModelHomePage
            {
                sliders = _servicesSlider.GetAll(),
                Setting = _ServicesSetting.GetAll(),
                books = new List<Book>()
            };

            // Fetch specific page data based on the ID
            if (id == 1)
            {
                viewModelHomePage.page = _servicesPage.GetById(Guid.Parse("BAE03428-3D59-4FCA-8F93-751E83913EF2"));
            }
            else if (id == 2)
            {
                viewModelHomePage.page = _servicesPage.GetById(Guid.Parse("A23D9995-FA1A-48C3-BA74-BA8B8AEC7875"));
            }
            else if (id == 3)
            {
                viewModelHomePage.page = _servicesPage.GetById(Guid.Parse("80F8BC57-22C4-4728-ACF0-9E381254437A"));
            }

            return View(viewModelHomePage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // #endregion
    }
}
