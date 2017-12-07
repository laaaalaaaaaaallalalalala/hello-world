using NorthernLights.CRM.Extensions;
using NorthernLights.CRM.Models;
using NorthernLights.CRM.Models.ProjectsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private RestController _service = new RestController();

        [HttpPost]
        public void AddTask(AddTaskViewModel model)
        {
            var record = new ToDo()
                {
                    ObjectType = typeof(Project).Name,
                    ObjectId = model.ProjectId,
                    CreatedBy = HttpContext.User.Identity.Name,
                    Name = model.Name,
                    Description = model.Description,
                    AssignedTo = model.AssignedTo,
                    DueDate = model.DueDate,
                    Status = model.TaskStatus,
                    DateUpdated = DateTime.UtcNow,
                };
            _service.AddToDo(record);
        }

        public ActionResult Create()
        {
            ViewBag.Type = new SelectList(_service.GetProjectTypeOptions(), "Name", "Description");
            ViewBag.Status = new SelectList(_service.GetProjectStatusOptions(), "Name", "Description");
            ViewBag.Industry = new SelectList(_service.GetIndustryOptions(), "Name", "Description");
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            ViewBag.Type = new SelectList(_service.GetProjectTypeOptions(), "Name", "Description");
            ViewBag.Status = new SelectList(_service.GetProjectStatusOptions(), "Name", "Description");
            ViewBag.Industry = new SelectList(_service.GetIndustryOptions(), "Name", "Description");
            ViewBag.Role = new SelectList(_service.GetRoleOptions(), "Name", "Description");
            ViewBag.TaskStatus = new SelectList(_service.GetToDoStatusOptions(), "Name", "Description");
            ViewBag.AssignedTo = new SelectList(_service.GetApplicationUsers(), "UserName", "UserName");
            return View();
        }

        public JsonResult GetStakeholders(int id)
        {
            return Json(_service.GetProjectStakeholders().Where(x => x.ProjectId == id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTasks(int id)
        {
            return Json(_service.GetToDoes().Where(x => x.ObjectType == typeof(Project).Name && x.ObjectId == id).ToList(),
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPut]
        public void UpdateProject(UpdateProjectViewModel model)
        {
            var record = _service.GetProjects().SingleOrDefault(x => x.Id == model.Id);
            if (record != null)
            {
                var changedValues = new List<ActivityViewModel.ChangedValue>();

                if (record.Name != model.Name)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Name", OldValue = record.Name, NewValue = model.Name });
                    record.Name = model.Name;
                }

                if (record.Type != model.Type)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Type", OldValue = record.Type, NewValue = model.Type });
                    record.Type = model.Type;
                }

                if (record.BeginDate != model.BeginDate)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Begin Date", OldValue = record.BeginDate, NewValue = model.BeginDate });
                    record.BeginDate = model.BeginDate;
                }

                if (record.AccountId != model.AccountId)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Account Id", OldValue = record.AccountId, NewValue = model.AccountId });
                    record.AccountId = model.AccountId;
                }

                if (record.EndDate != model.EndDate)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "End Date", OldValue = record.EndDate, NewValue = model.EndDate });
                    record.EndDate = model.EndDate;
                }

                if (record.Industry != model.Industry)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Industry", OldValue = record.Industry, NewValue = model.Industry });
                    record.Industry = model.Industry;
                }

                if (record.Status != model.Status)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Status", OldValue = record.Status, NewValue = model.Status });
                    record.Status = model.Status;
                }

                if (record.Description != model.Description)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Description", OldValue = record.Description, NewValue = model.Description });
                    record.Description = model.Description;
                }

                record.AppendActivity(new ActivityViewModel()
                {
                    Guid = Guid.NewGuid(),
                    DateCreated = DateTime.UtcNow,
                    UserName = HttpContext.User.Identity.Name,
                    Comment = model.Comment,
                    ChangedValues = changedValues,
                });

                _service.UpdateProject(record);
            }
        }
    }
}