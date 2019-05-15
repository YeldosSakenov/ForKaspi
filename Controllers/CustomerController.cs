using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    public class CustomerController : BaseController
    {
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Index()
        {
            return View();
        }
    }
}