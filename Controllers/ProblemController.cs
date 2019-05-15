using Abstractions.Interfaces.Entity;
using Core.Dtos;
using Core.Helpers;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    public class ProblemController : BaseController
    {
        IProblem db;
        ICategory _dbCategory;
        IUnit _unit;
        public ProblemController(IProblem iProblem, ICategory iCategory, IUnit unit)
        {
            db = iProblem;
            _dbCategory = iCategory;
            _unit = unit;
        }
        [Authorize(Roles = "Admin,Customer")]
        public  ActionResult Index(int? page)
        {
            ViewData["SelectedList"] = new SelectedListHelpers().GetSelectedList(_dbCategory.Get());        

            return View(db.Get().ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        public ActionResult Details(int id)
        {
            ViewData["SelectedList"] = new SelectedListHelpers().GetSelectedList(_dbCategory.Get());
            return View(db.Get(id));
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["SelectedList"] = new SelectedListHelpers().GetSelectedList(_dbCategory.Get());
            ViewData["SelectedListUnit"] = new SelectedListHelpers().GetSelectedList(_unit.Get());

            return View();
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        public async Task<ActionResult> Create(ProblemDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //await db.Create(value);

                    await db.CreateReletionUnit(value);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(value);
        }
        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["SelectedList"] = new SelectedListHelpers().GetSelectedList(_dbCategory.Get());

            return View(db.Get(id));
        }

        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> EditType(ProblemDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await db.Update(value);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ViewData["SelectedList"] = new SelectedListHelpers().GetSelectedList(_dbCategory.Get());
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(value);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        public async Task<ActionResult> Delete(ProblemDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await db.Delete(value);

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