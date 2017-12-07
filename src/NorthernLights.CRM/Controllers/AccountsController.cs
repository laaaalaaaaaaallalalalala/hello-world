using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private RestController _service = new RestController();

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            return View();
        }

        public JsonResult GetContacts(int id)
        {
            return Json(_service.GetContacts().Where(x => x.AccountId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjects(int id)
        {
            return Json(_service.GetProjects().Where(x => x.AccountId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UploadLogo()
        {
            var sourceFile = Request.Files[0];
            var targetUrl = string.Format("/Uploads/{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(sourceFile.FileName));
            var fileName = Server.MapPath(targetUrl);
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            sourceFile.SaveAs(fileName);
            return Json(new
            {
                path = targetUrl,
            });
        }
    }
}