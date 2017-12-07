using System;

namespace NorthernLights.CRM.Models
{
    public class EducationViewModel
    {
        public DateTime? BeginDate { get; set; }

        public string Certifications { get; set; }

        public string Degree { get; set; }

        public int EmployeeId { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? Id { get; set; }

        public string Major { get; set; }

        public string School { get; set; }
    }

    public class ExperienceViewModel
    {
        public DateTime? BeginDate { get; set; }

        public string Company { get; set; }

        public int EmployeeId { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? Id { get; set; }

        public string Projects { get; set; }

        public string Title { get; set; }
    }
}