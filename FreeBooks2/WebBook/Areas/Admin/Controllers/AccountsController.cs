using Bl.Data;
using Bl.ViewModel;
using Domains.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        #region Declaration
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FreeBookDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Constructor
        public AccountsController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
                                  FreeBookDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        #endregion

        #region Methods
        [Authorize(Roles = "Admin,Customer,SuperAdmin")]
        public IActionResult Roles()
        {
            return View(new RolesViewModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Roles(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.NewRole.RoleId,
                    Name = model.NewRole.Name,
                };

                // Create new role
                if (role.Id == Guid.Empty.ToString())
                {
                    role.Id = Guid.NewGuid().ToString();
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleSuccess, Resource.ResourceWeb.lbMsgSuccess);
                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbMsgError);
                    }
                }
                // Update existing role
                else
                {
                    var roleUpdate = await _roleManager.FindByIdAsync(role.Id);
                    roleUpdate.Name = role.Name;

                    var result = await _roleManager.UpdateAsync(roleUpdate);
                    if (result.Succeeded)
                    {
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbTittleEdit, Resource.ResourceWeb.lbMsgEdit);
                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbMsgEditError);
                    }
                }
            }
            return RedirectToAction("Roles");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role != null && (await _roleManager.DeleteAsync(role)).Succeeded)
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbTittleDeleteRoleSuccess);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbTittleDeleteRoleError);
            }
            return RedirectToAction("Roles");
        }

        [Authorize(Roles = "Admin,Customer,SuperAdmin")]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                Users = _context.VwUsers.OrderBy(x => x.Name).ToList(),
                NewUser = new NewUser(),
                ChangePassword = new ChangePasswordViewMoodel(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Register(RegisterViewModel model, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id = model.NewUser.Id,
                    Name = model.NewUser.Name,
                    Email = model.NewUser.Email,
                    UserName = model.NewUser.Email,
                    ActiveUser = model.NewUser.ActiveUser,
                };

                // Create new user
                if (model.NewUser.Id == Guid.Empty.ToString())
                {
                    user.ImageName = files.Count == 0 ? "default.jpg" : await Helper.UploadImage(files, "User");
                    user.Id = Guid.NewGuid().ToString();

                    var result = await _userManager.CreateAsync(user, model.NewUser.Password);
                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, model.NewUser.RoleName);
                        SessionMsg(roleResult.Succeeded ? Helper.Success : Helper.Error,
                                   Resource.ResourceWeb.lbTittleSuccess,
                                   roleResult.Succeeded ? Resource.ResourceWeb.lbMsgSuccessCreate : Resource.ResourceWeb.lbMsgError);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleError, Resource.ResourceWeb.lbMsgErrorCreate);
                    }
                }
                // Update existing user
                else
                {
                    var userUpdate = await _userManager.FindByIdAsync(user.Id);
                    userUpdate.ImageName = files.Count == 0 ? userUpdate.ImageName : await UpdateUserImage(files, userUpdate);
                    userUpdate.Name = user.Name;
                    userUpdate.UserName = user.Email;
                    userUpdate.Email = user.Email;
                    userUpdate.ActiveUser = user.ActiveUser;

                    var result = await _userManager.UpdateAsync(userUpdate);
                    if (result.Succeeded)
                    {
                        var role = await _userManager.GetRolesAsync(userUpdate);
                        await _userManager.RemoveFromRolesAsync(userUpdate, role);
                        var addRole = await _userManager.AddToRoleAsync(userUpdate, model.NewUser.RoleName);
                        SessionMsg(addRole.Succeeded ? Helper.Success : Helper.Error,
                                   Resource.ResourceWeb.lbTittleEdit,
                                   addRole.Succeeded ? Resource.ResourceWeb.lbMsgSuccessEdit : Resource.ResourceWeb.lbMsgEditError);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbTittleEditError, Resource.ResourceWeb.lbMsgErrorEdit);
                    }
                }
                return RedirectToAction("Register");
            }
            return RedirectToAction("Register");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (user != null && (await _userManager.DeleteAsync(user)).Succeeded)
            {
                SessionMsg(Helper.Success, Resource.ResourceWeb.lbDeleteSuccess, Resource.ResourceWeb.lbDeleteUserSuccess);
            }
            else
            {
                SessionMsg(Helper.Error, Resource.ResourceWeb.lbDeleteErrorTittle, Resource.ResourceWeb.lbDeleteUserSuccess);
            }
            return RedirectToAction("Register");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> ChangePassword(RegisterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.ChangePassword.Id);
            if (user != null)
            {
                await _userManager.RemovePasswordAsync(user);
                var result = await _userManager.AddPasswordAsync(user, model.ChangePassword.Password);
                SessionMsg(result.Succeeded ? Helper.Success : Helper.Error,
                           Resource.ResourceWeb.lbTittleEdit,
                           result.Succeeded ? Resource.ResourceWeb.lbMsgSuccessEditPassword : Resource.ResourceWeb.lbMsgEditError);
            }
            return RedirectToAction("Register");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorLogin = true;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        private void SessionMsg(string msgType, string title, string msg)
        {
            HttpContext.Session.SetString(Helper.msgType, msgType);
            HttpContext.Session.SetString(Helper.tittle, title);
            HttpContext.Session.SetString(Helper.msg, msg);
        }

        private async Task<string> UpdateUserImage(List<IFormFile> files, ApplicationUser user)
        {
            if (user.ImageName != "default.jpg")
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Images/User/" + user.ImageName);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            return await Helper.UploadImage(files, "User");
        }
        #endregion
    }
}
