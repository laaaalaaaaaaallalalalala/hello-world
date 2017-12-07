using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class CostsController : Controller
    {
        private RestController _service = new RestController();

        public decimal CalculateActualCost(int costConfigurationId, string type, decimal? hours)
        {
            var cc = _service.GetCostConfigurations().SingleOrDefault(x => x.Id == costConfigurationId);
            if (cc != null)
            {
                var rate = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(cc.Rates).SingleOrDefault(x => x.name == type);
                if (rate != null)
                {
                    return hours.GetValueOrDefault() * Convert.ToDecimal((rate.value as JValue).Value);
                }
            }

            return 0M;
        }

        public ActionResult Create()
        {
            ViewBag.ResourceTypes = _service.GetResourceTypeOptions();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            ViewBag.ResourceTypes = _service.GetResourceTypeOptions();
            ViewBag.ResourceType = new SelectList(_service.GetResourceTypeOptions(), "Name", "Description");
            return View();
        }

        public JsonResult GetCosts(int costConfigurationId)
        {
            return Json(_service.GetCosts().Where(x => x.CostConfigurationId == costConfigurationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProjects(int employeeId)
        {
            var data = from e in _service.GetEmployees()
                       join pc in _service.GetProjectStakeholders() on e.ContactId equals pc.ContactId
                       where e.Id == employeeId
                       select pc.Project;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReport(int costConfigurationId)
        {
            var data = new List<dynamic>();

            foreach (var x in _service.GetCosts().Where(x => x.CostConfigurationId == costConfigurationId))
            {
                data.Add(new
                {
                    ContactName = (x.Employee != null && x.Employee.Contact != null) ? x.Employee.Contact.Name : string.Empty,
                    ProjectName = (x.Project != null) ? x.Project.Name : string.Empty,
                    AccountCode = (x.Project != null && x.Project.Account != null) ? x.Project.Account.Code : string.Empty,
                    HeadCount = (x.CostConfiguration != null && x.CostConfiguration.PlannedHours > 0) ? x.ActualHours / x.CostConfiguration.PlannedHours : 1,
                    Rate = (x.CostConfiguration != null) ? this.GetRates(x.CostConfiguration.Rates).Where(y => y.Key == x.ResourceType).Select(y => y.Value).SingleOrDefault() : 0M,
                    ActualHours = x.ActualHours,
                    ActualCost = x.ActualCost,
                });
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        private IEnumerable<KeyValuePair<string, decimal>> GetRates(string json)
        {
            foreach (var element in JsonConvert.DeserializeObject<IEnumerable<dynamic>>(json))
            {
                yield return new KeyValuePair<string, decimal>(
                    Convert.ToString((element.name as JValue).Value),
                    Convert.ToDecimal((element.value as JValue).Value)
                );
            }
        }
    }
}