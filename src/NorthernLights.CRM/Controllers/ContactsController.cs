using System.Net;
using System.Web.Mvc;
using System.Linq;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private RestController _service = new RestController();

        public ActionResult Create()
        {
            ViewBag.Division = new SelectList(_service.GetDivisionOptions(), "Name", "Description");
            ViewBag.Gender = _service.GetGenderOptions();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            ViewBag.Division = new SelectList(_service.GetDivisionOptions(), "Name", "Description");
            ViewBag.Gender = _service.GetGenderOptions();
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProjects(int id)
        {
            return Json(_service.GetProjectStakeholders().Where(x => x.ContactId == id).Select(x => x.Project).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}