using Bl.IRepository;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SlidersController : Controller
    {
        // #region Dependencies
        private readonly IServicesRepositorySlider<Slider> _servicesSlider;

        public SlidersController(IServicesRepositorySlider<Slider> servicesSlider)
        {
            _servicesSlider = servicesSlider;
        }
        // #endregion

        // #region Actions
        public IActionResult Sliders()
        {
            SliderViewModel sliderViewModel = new SliderViewModel
            {
                Sliders = _servicesSlider.GetAll(),
                NewSlider = new Slider()
            };

            return View(sliderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SliderViewModel model, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (model.NewSlider.Id == Guid.Empty)
                {
                    // Create
                    if (files.Count == 0 || files == null)
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbSliderRequire);
                        return RedirectToAction("Sliders");
                    }
                    else
                    {
                        model.NewSlider.ImageName = await Helper.UploadImage(files, "Slider");
                    }

                    if (_servicesSlider.Save(model.NewSlider))
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleSuccess, Resource.ResourceWeb.lbSuccessCreateSlider);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbErrorCreateSlider);
                    }
                }
                else
                {
                    // Update
                    var oldSlider = _servicesSlider.GetById(model.NewSlider.Id);

                    if (files.Count == 0 || files == null)
                    {
                        model.NewSlider.ImageName = oldSlider.ImageName;
                    }
                    else
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/Slider/" + oldSlider.ImageName);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                        model.NewSlider.ImageName = await Helper.UploadImage(files, "Slider");
                    }

                    if (_servicesSlider.Save(model.NewSlider))
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleEdit, Resource.ResourceWeb.lbSuccessEditSlider);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbErrorEditSlider);
                    }
                }
            }
            return RedirectToAction("Sliders");
        }

        public IActionResult Delete(Guid id)
        {
            var oldSlider = _servicesSlider.GetById(id);
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/Slider/" + oldSlider.ImageName);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            if (_servicesSlider.Delete(id))
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbMsgSuccessDeleteSlider);
            else
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbMsgErrorDeleteSlider);

            return RedirectToAction("Sliders");
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
