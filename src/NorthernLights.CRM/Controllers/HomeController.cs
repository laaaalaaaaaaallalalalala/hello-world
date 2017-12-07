using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private RestController _service = new RestController();

        public JsonResult GetMyTasks()
        {
            return Json(_service.GetToDoes().Where(x => x.AssignedTo == HttpContext.User.Identity.Name).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActivities()
        {
            return Json((_service.GetAccounts().OrderByDescending(x => x.DateUpdated).Take(10).ToList().Select(x => new
            {
                Type = "Account",
                Id = x.Id,
                Name = x.Name,
                DateUpdated = x.DateUpdated.ToLocalTime(),
            })).Union(_service.GetContacts().OrderByDescending(x => x.DateUpdated).Take(10).ToList().Select(x => new
            {
                Type = "Contact",
                Id = x.Id,
                Name = x.Name,
                DateUpdated = x.DateUpdated.ToLocalTime(),
            })).Union(_service.GetProjects().OrderByDescending(x => x.DateUpdated).Take(10).ToList().Select(x => new
            {
                Type = "Project",
                Id = x.Id,
                Name = x.Name,
                DateUpdated = x.DateUpdated.ToLocalTime(),
            })).Union(_service.GetEmployees().OrderByDescending(x => x.DateUpdated).Take(10).ToList().Select(x => new
            {
                Type = "Employee",
                Id = x.Id,
                Name = x.Contact.Name,
                DateUpdated = x.DateUpdated.ToLocalTime(),
            })).Union(_service.GetCostConfigurations().OrderByDescending(x => x.DateUpdated).Take(10).ToList().Select(x => new
            {
                Type = "CostConfiguration",
                Id = x.Id,
                Name = x.Name,
                DateUpdated = x.DateUpdated.ToLocalTime(),
            })).OrderByDescending(x => x.DateUpdated).Take(10).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}