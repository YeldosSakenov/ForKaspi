using Abstractions.Interfaces.Entity;
using Abstractions.Interfaces.Helpers;
using IdeaBank.Web.Controllers;
using PagedList;
using System.Web.Mvc;

namespace IdeaBank.Controllers
{
    public class HomeController : BaseController
    {
        private IOffer db;
        IStatusController _iStatusController;
        public HomeController(IOffer iOffer, IStatusController iStatusController)
        {
            db = iOffer;
            _iStatusController = iStatusController;
        }

        public ActionResult Index(int? page)
        {           
            return View(db.Get().ToPagedList(page ?? 1, 10  ));          
        }
     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}