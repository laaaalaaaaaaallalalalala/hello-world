using System;

namespace NorthernLights.CRM.Models.ToDoesViewModels
{
    public class UpdateToDoViewModel
    {
        public string AssignedTo { get; set; }

        public string Comment { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string TaskStatus { get; set; }
    }
}