using NorthernLights.CRM.Extensions;
using NorthernLights.CRM.Models;
using NorthernLights.CRM.Models.ToDoesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class ToDoesController : Controller
    {
        private RestController _service = new RestController();

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            ViewBag.TaskStatus = new SelectList(_service.GetToDoStatusOptions(), "Name", "Description");
            ViewBag.AssignedTo = new SelectList(_service.GetApplicationUsers(), "UserName", "UserName");
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPut]
        public void UpdateToDo(UpdateToDoViewModel model)
        {
            var record = _service.GetToDoes().SingleOrDefault(x => x.Id == model.Id);
            if (record != null)
            {
                var changedValues = new List<ActivityViewModel.ChangedValue>();

                if (record.Name != model.Name)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Name", OldValue = record.Name, NewValue = model.Name });
                    record.Name = model.Name;
                }

                if (record.AssignedTo != model.AssignedTo)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Assigned To", OldValue = record.AssignedTo, NewValue = model.AssignedTo });
                    record.AssignedTo = model.AssignedTo;
                }

                if (record.DueDate != model.DueDate)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Due Date", OldValue = record.DueDate, NewValue = model.DueDate });
                    record.DueDate = model.DueDate;
                }

                if (record.Status != model.TaskStatus)
                {
                    changedValues.Add(new ActivityViewModel.ChangedValue() { Key = "Status", OldValue = record.Status, NewValue = model.TaskStatus });
                    record.Status = model.TaskStatus;
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

                _service.UpdateToDo(record);
            }
        }
    }
}