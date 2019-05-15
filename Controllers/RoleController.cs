using Abstractions.Interfaces.Entity;
using Core.Dtos;
using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        IAspNetRole _iAspNetRole;

        public RoleController(IAspNetRole aspNetRole)
        {
            _iAspNetRole = aspNetRole;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            return View(_iAspNetRole.GetAspNetRoles().ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult Details(string id)
        {
            return View(_iAspNetRole.GetAspNetRole(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public  ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(AspNetRoleDto role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iAspNetRole.Create(role);

                    return RedirectToAction("Index");
                }
            }
            catch 
            {               
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(role);
        }
        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpGet]
        public ActionResult Edit(string id)
        {           
            return View(_iAspNetRole.GetAspNetRole(id));
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> EditRole(AspNetRoleDto role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iAspNetRole.Update(role);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(role);
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        public async Task<ActionResult> Delete(AspNetRoleDto role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _iAspNetRole.Delete(role);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View("index");
        }

    }
}