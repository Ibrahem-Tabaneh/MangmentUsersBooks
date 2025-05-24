using Bl.IRepository;
using Bl.IRepository.ServicesRepository;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        // #region Dependencies
        private readonly IServicesRepository<Book> _servicesbook;
        private readonly IServicesRepository<Category> _servicescategory;
        private readonly IServicesRepositoryLog<LogBook> _serviceslogbook;
        private readonly IServicesRepository<SubCategory> _servicessubcategory;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(IServicesRepository<Book> servicesbook,
                               IServicesRepository<Category> servicescategory,
                               IServicesRepositoryLog<LogBook> serviceslogbook,
                               IServicesRepository<SubCategory> servicessubcategory,
                               UserManager<ApplicationUser> userManager)
        {
            _servicesbook = servicesbook;
            _servicescategory = servicescategory;
            _serviceslogbook = serviceslogbook;
            _servicessubcategory = servicessubcategory;
            _userManager = userManager;
        }
        // #endregion

        // #region Actions
        [Authorize(Roles = "Admin,Customer,SuperAdmin")]
        public IActionResult Books()
        {
            var bookViewModel = new BookViewModel
            {
                Books = _servicesbook.GetAll(),
                NewBook = new Book(),
                Categories = _servicescategory.GetAll(),
                LogBooks = _serviceslogbook.GetAll(),
                SubCategories = _servicessubcategory.GetAll()
            };
            return View(bookViewModel);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(BookViewModel model, List<IFormFile> Files, List<IFormFile> FilesAuthor, List<IFormFile> FilesFileName)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (model.NewBook.Id == Guid.Empty)
                {
                    // Create new book
                    if (_servicesbook.FindById(model.NewBook.Name) != null)
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbMsgNameBookExists);
                        return RedirectToAction("Books");
                    }

                    // Upload book image
                    model.NewBook.ImageName = Files.Count == 0 ? "defaultbook.png" : await Helper.UploadImage(Files, "Book");

                    // Upload author image
                    model.NewBook.ImageAuthor = FilesAuthor.Count == 0 ? "default.jpg" : await Helper.UploadImage(FilesAuthor, "Author");

                    // Upload PDF
                    if (FilesFileName.Count == 0)
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbErrorSelectPdf);
                        return RedirectToAction("Books");
                    }
                    model.NewBook.FileName = await Helper.UploadPdf(FilesFileName, "pdf");

                    // Save book and log
                    if (_servicesbook.Save(model.NewBook) && _serviceslogbook.Save(model.NewBook.Id, Guid.Parse(userId)))
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleSuccess, Resource.ResourceWeb.lbSuccessCreateBook);
                        return RedirectToAction("Books");
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbErrorCreateBook);
                    }
                }
                else
                {
                    // Update existing book
                    var existingBook = _servicesbook.FindById(model.NewBook.Name);
                    if (existingBook != null && existingBook.Id != model.NewBook.Id)
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbMsgNameBookExists);
                        return RedirectToAction("Books");
                    }

                    var oldBook = _servicesbook.FindById(model.NewBook.Id);

                    // Update book image
                    model.NewBook.ImageName = Files.Count == 0 ? oldBook.ImageName : await UpdateImage(Files, "Book", oldBook.ImageName);

                    // Update author image
                    model.NewBook.ImageAuthor = FilesAuthor.Count == 0 ? oldBook.ImageAuthor : await UpdateImage(FilesAuthor, "Author", oldBook.ImageAuthor);

                    // Update PDF
                    model.NewBook.FileName = FilesFileName.Count == 0 ? oldBook.FileName : await UpdatePdf(FilesFileName, oldBook.FileName);

                    // Save updated book and log
                    if (_servicesbook.Save(model.NewBook) && _serviceslogbook.Update(model.NewBook.Id, Guid.Parse(userId)))
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbEditSuccess, Resource.ResourceWeb.lbSuccessEditBook);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbErrorEditBook);
                    }
                }
            }

            return RedirectToAction("Books");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            if (_servicesbook.Delete(id) && _serviceslogbook.Delete(id, Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteBook);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteBook);
            }

            return RedirectToAction("Books");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult DeleteLog(Guid id)
        {
            if (_serviceslogbook.DeleteLog(id))
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteCategoryLog);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteCategoryLog);
            }
            return RedirectToAction("Books");
        }
        // #endregion

        // #region Helper Methods
        private async Task<string> UpdateImage(List<IFormFile> files, string type, string oldImageName)
        {
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Images/{type}/" + oldImageName);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            return await Helper.UploadImage(files, type);
        }

        private async Task<string> UpdatePdf(List<IFormFile> files, string oldFileName)
        {
            var oldPdfPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/PdfFiles/pdf/" + oldFileName);
            if (System.IO.File.Exists(oldPdfPath))
            {
                System.IO.File.Delete(oldPdfPath);
            }
            return await Helper.UploadPdf(files, "pdf");
        }

        private void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.msgType, msgType);
            HttpContext.Session.SetString(Helper.tittle, title);
            HttpContext.Session.SetString(Helper.msg, msg);
        }
        // #endregion
    }
}
