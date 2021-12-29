using CoursesProject.Data;
using CoursesProject.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Setting
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManeger;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManeger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManeger = roleManeger;
        }
        #endregion

        #region User
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Courses");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.UserEmail,
                    UserName = model.UserEmail.Split('@')[0],
                    PhoneNumber = model.PhoneNumber,

                    City = model.City,
                    Gender = model.Gender,
                    Address = model.Address
                };


                var result = await _userManager.CreateAsync(user, model.UserPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Courses");
                }

                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, errors.Description);
                }

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.UserPassword, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Wrong User/Password");
                }
                return View(model);
            }
            catch (Exception err)
            {
                ViewBag.err = err.Message;
                return NoContent();
            }

        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region Role
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName,

                };

                var result = await _roleManeger.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult ListRoles()
        {
            IQueryable<IdentityRole> roles = _roleManeger.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditeRole(string id)
        {
            IdentityRole TargetRole = await _roleManeger.FindByIdAsync(id);
            if (TargetRole == null)
            {
                return View("NotFound");
            }

            EdditRoleViewModel model = new EdditRoleViewModel
            {
                Id = TargetRole.Id,
                RoleName = TargetRole.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, TargetRole.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditeRole(EdditRoleViewModel model)
        {
            IdentityRole TargetRole = await _roleManeger.FindByIdAsync(model.Id);
            if (TargetRole == null)
            {
                return View("NotFound");

            }
            else
            {
                TargetRole.Name = model.RoleName;

                IdentityResult result = await _roleManeger.UpdateAsync(TargetRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.roleID = id;
            var TargetRole = await _roleManeger.FindByIdAsync(id);
            if (TargetRole == null)
            {
                return View("NotFound");
            }
            ViewBag.RoleName = TargetRole.Name;
            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, TargetRole.Name /*OR role.Id*/ ))
                {
                    userRoleViewModel.isSelected = true;
                }
                else
                {
                    userRoleViewModel.isSelected = false;
                }
                model.Add(userRoleViewModel);

            }
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> users, string roleID)
        {
            IdentityRole TargetRole = await _roleManeger.FindByIdAsync(roleID);
            if (TargetRole is null)
            {
                return View("NotFound");
            }
            for (int i = 0; i < users.Count; i++)
            {
                IdentityResult result = null;
                var user = await _userManager.FindByIdAsync(users[i].UserId);
                if (users[i].isSelected && !(await _userManager.IsInRoleAsync(user, TargetRole.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, TargetRole.Name);
                }
                else if (!users[i].isSelected && await _userManager.IsInRoleAsync(user, TargetRole.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, TargetRole.Name);
                }
                else { continue; }

                if (result.Succeeded)
                    continue;
            }

            return RedirectToAction("ListRoles");

        }

        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }


        #endregion

        #region ChangePassword

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword, model.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }

            return View(model);
        }

        #endregion
    }
}
