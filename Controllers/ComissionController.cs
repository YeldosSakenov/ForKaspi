using Abstractions.Interfaces.Entity;
using Abstractions.Interfaces.Helpers;
using Core.Dtos;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    public class ComissionController : BaseController
    {
        private IApproved db;
        private IAspNetUser _users;
        private IOffer _dbOffer;
        private IStatusController _iStatusController;
        public ComissionController(IApproved iApproved, IAspNetUser users, IOffer dbOffer, IStatusController iStatusControlle)
        {
            db = iApproved;
            _users = users;
            _dbOffer = dbOffer;
            _iStatusController = iStatusControlle;
        }

        [Authorize(Roles = "Admin,Comission")]
        public ActionResult Index(int? page)
        {
            return View(_dbOffer.Get().ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin,Comission")]
        [HandleError]
        public async Task<ActionResult> Details(int id)
        {
            IEnumerable<ApprovedDto> approveds = await db.Get(new
                ApprovedDto
            {
                OfferId = id,
                UserId = _users.GetAspNetUsers().ToList()
                .Where(u => u.Email == User.Identity.GetUserName())
                .First().Id
            });
            return View(approveds);
        }
        [Authorize(Roles = "Admin,Comission")]
        [HandleError]
        public async Task<ActionResult> Approve(int id, int? page)
        {
            await db.Create(new ApprovedDto
            {
                OfferId = id,
                StatusId = 4,
                UserId = _users.GetAspNetUsers().ToList()
                  .Where(u => u.Email == User.Identity.GetUserName())
                  .First().Id
            });

            if (_iStatusController.CheckStatus(id))
            {
                OfferDto offer = _dbOffer.Get(id);
                offer.StatusesId = 4;
                await _dbOffer.Update(offer);
            }

            return View("Index", _dbOffer.Get().ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin,Comission")]
        [HandleError]
        public async Task<ActionResult> Dismiss(int id, int? page)
        {
            await db.Create(new ApprovedDto
            {
                OfferId = id,
                StatusId = 3,
                UserId = _users.GetAspNetUsers().ToList()
               .Where(u => u.Email == User.Identity.GetUserName())
               .First().Id
            });

            return View("Index", _dbOffer.Get().ToPagedList(page ?? 1, 10));
        }

    }

}