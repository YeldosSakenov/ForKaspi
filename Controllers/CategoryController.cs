﻿using Abstractions.Interfaces.Entity;
using Core.Dtos;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    public class CategoryController : BaseController
    {
        ICategory db;

        public CategoryController(ICategory iCategory)
        {
            db = iCategory;
        }
        [Authorize(Roles = "Admin,Customer")]
        // GET: Role
        public ActionResult Index(int? page)
        {
            return View(db.Get().ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        public ActionResult Details(int id)
        {  
            return View(db.Get(id));
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDto value)
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
        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Get(id));
        }

        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> EditType(CategoryDto value)
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

        [Authorize(Roles = "Admin,Customer")]
        [HandleError]
        public async Task<ActionResult> Delete(CategoryDto value)
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