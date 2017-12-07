using Newtonsoft.Json;
using NorthernLights.CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private RestController _service = new RestController();

        [HttpPost]
        public void AddOrUpdateEducation(EducationViewModel model)
        {
            var record = _service.GetEmployees().SingleOrDefault(x => x.Id == model.EmployeeId);
            if (record != null)
            {
                var models = new List<EducationViewModel>();
                if (!string.IsNullOrWhiteSpace(record.Education))
                {
                    models.AddRange(JsonConvert.DeserializeObject<IEnumerable<EducationViewModel>>(record.Education));
                }

                if (model.Id == null)
                {
                    model.Id = Guid.NewGuid();
                    models.Add(model);
                }
                else
                {
                    models.Remove(models.SingleOrDefault(x => x.Id == model.Id));
                    models.Add(model);
                }

                record.Education = JsonConvert.SerializeObject(models);
                _service.UpdateEmployee(record);
            }
        }

        [HttpPost]
        public void AddOrUpdateExperience(ExperienceViewModel model)
        {
            var record = _service.GetEmployees().SingleOrDefault(x => x.Id == model.EmployeeId);
            if (record != null)
            {
                var models = new List<ExperienceViewModel>();
                if (!string.IsNullOrWhiteSpace(record.Experience))
                {
                    models.AddRange(JsonConvert.DeserializeObject<IEnumerable<ExperienceViewModel>>(record.Experience));
                }

                if (model.Id == null)
                {
                    model.Id = Guid.NewGuid();
                    models.Add(model);
                }
                else
                {
                    models.Remove(models.SingleOrDefault(x => x.Id == model.Id));
                    models.Add(model);
                }

                record.Experience = JsonConvert.SerializeObject(models);
                _service.UpdateEmployee(record);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Division = new SelectList(_service.GetDivisionOptions(), "Name", "Description");
            ViewBag.TechnicalSkills = new SelectList(_service.GetSkillOptions(), "Name", "Description");
            return View();
        }

        [HttpPost]
        public void DeleteEducation(string id, int employeeId)
        {
            var record = _service.GetEmployees().SingleOrDefault(x => x.Id == employeeId);
            if (record != null && !string.IsNullOrWhiteSpace(record.Education))
            {
                var models = JsonConvert.DeserializeObject<IEnumerable<EducationViewModel>>(record.Education).ToList();
                models.Remove(models.SingleOrDefault(x => x.Id == new Guid(id)));
                record.Education = JsonConvert.SerializeObject(models);
                _service.UpdateEmployee(record);
            }
        }

        [HttpPost]
        public void DeleteExperience(string id, int employeeId)
        {
            var record = _service.GetEmployees().SingleOrDefault(x => x.Id == employeeId);
            if (record != null && !string.IsNullOrWhiteSpace(record.Experience))
            {
                var models = JsonConvert.DeserializeObject<IEnumerable<ExperienceViewModel>>(record.Experience).ToList();
                models.Remove(models.SingleOrDefault(x => x.Id == new Guid(id)));
                record.Experience = JsonConvert.SerializeObject(models);
                _service.UpdateEmployee(record);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            ViewBag.Division = new SelectList(_service.GetDivisionOptions(), "Name", "Description");
            ViewBag.TechnicalSkills = new SelectList(_service.GetSkillOptions(), "Name", "Description");
            ViewBag.Degree = new SelectList(_service.GetDegreeOptions(), "Name", "Description");
            return View();
        }

        public JsonResult GetContactsForCreatingEmployee()
        {
            var contacts = from c in _service.GetContacts()
                           join e in _service.GetEmployees() on c.Id equals e.ContactId into g
                           where g.Count() == 0
                           select c;

            return Json(contacts.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEducation(string id, int employeeId)
        {
            var record = _service.GetEmployees().SingleOrDefault(x => x.Id == employeeId);
            if (record != null && !string.IsNullOrWhiteSpace(record.Education))
            {
                var models = JsonConvert.DeserializeObject<IEnumerable<EducationViewModel>>(record.Education);
                return Json(models.SingleOrDefault(x => x.Id == new Guid(id)), JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetExperience(string id, int employeeId)
        {
            var record = _service.GetEmployees().SingleOrDefault(x => x.Id == employeeId);
            if (record != null && !string.IsNullOrWhiteSpace(record.Experience))
            {
                var models = JsonConvert.DeserializeObject<IEnumerable<ExperienceViewModel>>(record.Experience);
                return Json(models.SingleOrDefault(x => x.Id == new Guid(id)), JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}