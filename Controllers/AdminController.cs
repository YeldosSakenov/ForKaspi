using Abstractions.Interfaces.Entity;
using Core.Dtos;
using Core.Helpers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    public class AdminController : BaseController
    {        
        private IAspNetUser _iAspNetUser;
        private IAspNetRole _iAspNetRole;
        private IAspNetUserRoles _iAspNetUserRoles;
        public AdminController(IAspNetUser iAspNetUser, IAspNetRole iAspNetRole, IAspNetUserRoles iAspNetUserRoles)
        {
            _iAspNetUser = iAspNetUser;
            _iAspNetRole = iAspNetRole;
            _iAspNetUserRoles = iAspNetUserRoles;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdministrationUsers()
        {
            return View(_iAspNetUser.GetAspNetUsers());
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        public async Task<ActionResult> Delete(AspNetUserDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iAspNetUser.Delete(value);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View("AdministrationUsers", _iAspNetUser.GetAspNetUsers());
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        public async Task<ActionResult> UserRolesControl(AspNetUserDto value)
        {
            ViewData["SelectedListForRoles"] = new SelectedListHelpers().GetSelectedList(_iAspNetRole.GetAspNetRoles());
            ViewData["UserId"] = value.Id;

            return View(await _iAspNetUserRoles.Get(new AspNetUserRolesDto { UserId = value.Id }));
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddRole(AspNetUserRolesDto value)
        {
            ViewData["SelectedListForRoles"] = new SelectedListHelpers().GetSelectedList(_iAspNetRole.GetAspNetRoles());
            try
            {
                if (ModelState.IsValid)
                {
                    await _iAspNetUserRoles.Create(value);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("UserRolesControl", new AspNetUserDto { Id = value.UserId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRole(AspNetUserRolesDto value)
        {
            ViewData["SelectedListForRoles"] = new SelectedListHelpers().GetSelectedList(_iAspNetRole.GetAspNetRoles());
            try
            {
                if (ModelState.IsValid)
                {
                    await _iAspNetUserRoles.Delete(value);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("UserRolesControl", new AspNetUserDto { Id = value.UserId });
        }

    }
}