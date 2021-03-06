﻿using Abstractions.Interfaces.Entity;
using Core.Dtos;
using PagedList;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    public class UnitController : BaseController
    {
        IUnit db;

        public UnitController(IUnit iUnit)
        {
            db = iUnit;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {
            return View(db.Get().ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin")]
        [HandleError]
        public ActionResult Details(int id)
        {
            return View(db.Get(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(UnitDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await db.Create(value);

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(value);
        }
        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Get(id));
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> EditType(UnitDto value)
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
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(value);
        }

        [Authorize(Roles = "Admin")]
        [HandleError]
        public async Task<ActionResult> Delete(UnitDto value)
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