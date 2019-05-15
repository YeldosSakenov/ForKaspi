using Abstractions.Interfaces.Entity;
using Core.Dtos;
using Core.Helpers;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdeaBank.Web.Controllers
{
    [Authorize]
    public class OfferController : BaseController
    {
        private IOffer db;
        private ITypeOfOffer _iTypeOfOffer;
        private IProblem _iProblem;
        private IStatus _iStatus;
        private IDirection _iDirection;
        private IAspNetUser _users;
        public OfferController(IOffer iOffer, ITypeOfOffer iTypeOfOffer, IProblem iProblem, IStatus iStatus, IDirection iDirection, IAspNetUser users)
        {
            db = iOffer;
            _iTypeOfOffer = iTypeOfOffer;
            _iProblem = iProblem;
            _iStatus = iStatus;
            _iDirection = iDirection;
            _users = users;
        }
        [Authorize(Roles = "Admin,Autor")]
        public ActionResult Index(int? page)
        {
            return View(db.Get(User.Identity.GetUserName()).ToPagedList(page ?? 1, 10));
        }
        [Authorize(Roles = "Admin,Autor")]
        [HandleError]
        public ActionResult Details(int id)
        {            
            return View(db.Get(id));
        }
        [Authorize(Roles = "Admin,Autor")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["SelectedListForTypeOfOffer"] = new SelectedListHelpers().GetSelectedList(_iTypeOfOffer.Get());
            ViewData["SelectedListForProblem"] = new SelectedListHelpers().GetSelectedList(_iProblem.Get());
            //ViewData["SelectedListForStatus"] = new SelectedListHelpers().GetSelectedList(_iStatus.Get());
            ViewData["SelectedListForDirection"] = new SelectedListHelpers().GetSelectedList(_iDirection.Get());

            return View();
        }

        [Authorize(Roles = "Admin,Autor")]
        [HttpPost]
        public async Task<ActionResult> Create(OfferDto value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (value.FileContent != null)
                    {
                        List <FileDto> files= new List<FileDto>();
                        foreach (var image in value.FileContent)
                        {
                            using (var br = new BinaryReader(image.InputStream))
                            {
                                FileDto fileDto = new FileDto {
                                    FileContent = br.ReadBytes(image.ContentLength),
                                    FileName = image.FileName,
                                    ContentType=image.ContentType

                                };
                                files.Add(fileDto);
                               
                            }
                        }
                        value.Files = files;
                    }
                    value.UserId = _users.GetAspNetUsers().ToList().Where(u=>u.UserName== User.Identity.GetUserName()).First().Id;
                    value.StatusesId = 2;

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
        [Authorize(Roles = "Admin,Autor")]
        [HandleError]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewData["SelectedListForTypeOfOffer"] = new SelectedListHelpers().GetSelectedList(_iTypeOfOffer.Get());
            ViewData["SelectedListForProblem"] = new SelectedListHelpers().GetSelectedList(_iProblem.Get());
            //ViewData["SelectedListForStatus"] = new SelectedListHelpers().GetSelectedList(_iStatus.Get());
            ViewData["SelectedListForDirection"] = new SelectedListHelpers().GetSelectedList(_iDirection.Get());
           
            return View(db.Get(id));
        }

        [Authorize(Roles = "Admin,Autor")]
        [HandleError]
        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> EditType(OfferDto value, int FilesId=0)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (value.FileContent != null)
                    {
                        List<FileDto> files = new List<FileDto>();
                       
                        foreach (var image in value.FileContent)
                        {
                            using (var br = new BinaryReader(image.InputStream))
                            {
                                FileDto fileDto = new FileDto
                                {
                                    FileContent = br.ReadBytes(image.ContentLength),
                                    FileName = image.FileName,
                                    ContentType = image.ContentType,
                                    OfferId = value.Id,
                                    Id = FilesId
                                };
                                files.Add(fileDto);

                            }
                        }
                        value.Files = files;
                    }

                    value.UserId = _users.GetAspNetUsers().ToList().Where(u => u.UserName == User.Identity.GetUserName()).First().Id;
                    value.StatusesId = 2;

                    await db.Update(value);

                    return RedirectToAction("Index");
                }
                else {
                    ViewData["SelectedListForTypeOfOffer"] = new SelectedListHelpers().GetSelectedList(_iTypeOfOffer.Get());
                    ViewData["SelectedListForProblem"] = new SelectedListHelpers().GetSelectedList(_iProblem.Get());
                    //ViewData["SelectedListForStatus"] = new SelectedListHelpers().GetSelectedList(_iStatus.Get());
                    ViewData["SelectedListForDirection"] = new SelectedListHelpers().GetSelectedList(_iDirection.Get());
                }                
            }
            catch
            {               
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(value);
        }

        [Authorize(Roles = "Admin,Autor")]
        [HandleError]
        public async Task<ActionResult> Delete(OfferDto value)
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

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            byte[] bytes;          
           
            foreach (var file in files)
            {
                FileDto FileDto = new FileDto();

                string filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/UploadedFiles"), filePath));                
               
                using (BinaryReader br = new BinaryReader(file.InputStream))
                {
                    bytes = br.ReadBytes(file.ContentLength);
                }
              
            }
            return Json("file uploaded successfully");
        }

       
        public FileResult DownloadFile(int id)
        {
            byte[] bytes=null;
            string fileName="", contentType="";

            var offer = db.Get(id);

            if (offer.Files.Count != 0)
            {
                contentType= offer.Files.First().ContentType;
                fileName = offer.Files.First().FileName;
                bytes = offer.Files.First().FileContent;
            }
            return File(bytes, contentType, fileName);
        }
    }
}