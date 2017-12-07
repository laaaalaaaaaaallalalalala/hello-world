using Newtonsoft.Json;
using NorthernLights.CRM.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NorthernLights.CRM.Models
{
    public enum OptionCategoryEnum
    {
        ResourceType = 1,
        Role = 2,
        Division = 3,
        Industry = 4,
        Skill = 5,
        Gender = 6,
        ProjectStatus = 7,
        ProjectType = 8,
        Degree = 9,
        TodoStatus = 10,
    }

    public class Account
    {
        public string Code { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }
    }

    public class Contact
    {
        public virtual Account Account { get; set; }

        public int? AccountId { get; set; }

        public string Alias { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string Gender { get; set; }

        public string Hobby { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string LastName { get; set; }

        public string Mobile { get; set; }

        public string Name
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Alias)
                    ? string.Format("{0} {1}", this.FirstName, this.LastName)
                    : this.Alias;
            }
        }

        public string SocialAccounts { get; set; }

        public string Title { get; set; }
    }

    public class Cost
    {
        public decimal ActualCost { get; set; }

        public decimal ActualHours { get; set; }

        public string Alias { get; set; }

        public string Comment { get; set; }

        public virtual CostConfiguration CostConfiguration { get; set; }

        public int CostConfigurationId { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public virtual Employee Employee { get; set; }

        public int EmployeeId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Project Project { get; set; }

        public int ProjectId { get; set; }

        public string ResourceType { get; set; }
    }

    public class CostConfiguration
    {
        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Month { get; set; }

        public string Name { get; set; }

        public int PlannedHours { get; set; }

        public string Rates { get; set; }

        public int Year { get; set; }
    }

    public class CrmDbContext : DbContext
    {
        static CrmDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrmDbContext, Configuration>());
        }

        public CrmDbContext()
            : base("CrmConnection")
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<CostConfiguration> CostConfigurations { get; set; }

        public DbSet<Cost> Costs { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<ProjectManagement> ProjectManagements { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectStakeholder> ProjectStakeholders { get; set; }

        public DbSet<ToDo> ToDoes { get; set; }

        public static CrmDbContext Create()
        {
            return new CrmDbContext();
        }
    }

    public class Employee
    {
        public DateTime? BeginDate { get; set; }

        public string Comment { get; set; }

        public virtual Contact Contact { get; set; }

        public int ContactId { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Division { get; set; }

        public string Education { get; set; }

        public DateTime? EndDate { get; set; }

        public string Experience { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Skills { get; set; }

        public IEnumerable<string> TechnicalSkills
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Skills))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<string>>(this.Skills);
                }

                return Enumerable.Empty<string>();
            }
            set
            {
                this.Skills = JsonConvert.SerializeObject(value);
            }
        }
    }

    public class Option
    {
        public OptionCategoryEnum Category { get; set; }

        public DateTime? DateDeleted { get; set; }

        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Ordinal { get; set; }
    }

    public class Project
    {
        public virtual Account Account { get; set; }

        public int? AccountId { get; set; }

        public string Activities { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Description { get; set; }

        public DateTime? EndDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Industry { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }

    public class ProjectManagement
    {
        public string CommunicationPlan { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Project Project { get; set; }

        public int ProjectId { get; set; }

        public string StakeholderPlan { get; set; }
    }

    public class ProjectStakeholder
    {
        public DateTime? BeginDate { get; set; }

        public string Comment { get; set; }

        public virtual Contact Contact { get; set; }

        public int ContactId { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime? EndDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Project Project { get; set; }

        public int ProjectId { get; set; }

        public string Role { get; set; }
    }

    public class ToDo
    {
        public string Activities { get; set; }

        public string AssignedTo { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? DateDeleted { get; set; }

        public DateTime DateUpdated { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ObjectId { get; set; }

        public string ObjectType { get; set; }

        public string Status { get; set; }
    }
}