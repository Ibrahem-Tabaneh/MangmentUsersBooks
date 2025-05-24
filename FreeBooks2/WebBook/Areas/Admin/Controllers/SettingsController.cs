using Bl.Data;
using Bl.IRepository;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SettingsController : Controller
    {
        // #region Dependencies
        public IServicesRepositorySetting<Setting> _ServicesSetting { get; }

        public SettingsController(IServicesRepositorySetting<Setting> servicesSetting)
        {
            _ServicesSetting = servicesSetting;
        }
        // #endregion

        // #region Actions
        public IActionResult Settings()
        {
            Setting setting = _ServicesSetting.GetAll();
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Setting setting, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var oldSetting = _ServicesSetting.GetAll();

                // هنا تعيين الشعار إذا كان هناك ملفات
                if (files.Count == 0 || files == null)
                {
                    oldSetting.logo = oldSetting.logo; // لا تغيير على الشعار
                }
                else
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/Setting/" + oldSetting.logo);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    oldSetting.logo = await Helper.UploadImage(files, "Setting");
                }

                if (_ServicesSetting.Save(setting))
                {
                    SessionMsg(Helper.Success, Resource.ResourceWeb.lbEditSuccess, Resource.ResourceWeb.lbEditSettingSuccess);
                }
                else
                {
                    SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbEditSettingError);
                }
            }

            return RedirectToAction("Settings");
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
