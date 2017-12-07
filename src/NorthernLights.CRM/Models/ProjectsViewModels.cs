using System;
using System.Collections.Generic;

namespace NorthernLights.CRM.Models
{
    public class ActivityViewModel
    {
        public IEnumerable<ChangedValue> ChangedValues { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid Guid { get; set; }

        public string UserName { get; set; }

        public class ChangedValue
        {
            public string Key { get; set; }

            public object NewValue { get; set; }

            public object OldValue { get; set; }
        }
    }
}

namespace NorthernLights.CRM.Models.ProjectsViewModels
{
    public class AddTaskViewModel
    {
        public string AssignedTo { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int? Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public string TaskStatus { get; set; }
    }

    public class UpdateProjectViewModel
    {
        public int? AccountId { get; set; }

        public DateTime? BeginDate { get; set; }

        public string Comment { get; set; }

        public string Description { get; set; }

        public DateTime? EndDate { get; set; }

        public int Id { get; set; }

        public string Industry { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}