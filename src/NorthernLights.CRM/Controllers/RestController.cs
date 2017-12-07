using NorthernLights.CRM.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NorthernLights.CRM.Controllers
{
    [Authorize]
    public class RestController : ApiController
    {
        private CrmDbContext db = new CrmDbContext();
        private ApplicationDbContext identity = new ApplicationDbContext();

        [HttpPost]
        [ResponseType(typeof(Account))]
        public IHttpActionResult AddAccount(Account record)
        {
            record.DateUpdated = DateTime.UtcNow;
            db.Set<Account>().Add(record);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult AddContact(Contact record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.DateUpdated = DateTime.UtcNow;
            db.Set<Contact>().Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(Cost))]
        public IHttpActionResult AddCost(Cost record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.DateUpdated = DateTime.UtcNow;
            db.Set<Cost>().Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(CostConfiguration))]
        public IHttpActionResult AddCostConfiguration(CostConfiguration record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.DateUpdated = DateTime.UtcNow;
            db.Set<CostConfiguration>().Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult AddEmployee(Employee record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.DateUpdated = DateTime.UtcNow;
            db.Set<Employee>().Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(Project))]
        public IHttpActionResult AddProject(Project record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.DateUpdated = DateTime.UtcNow;
            db.Set<Project>().Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(ProjectStakeholder))]
        public IHttpActionResult AddProjectStakeholder(ProjectStakeholder record)
        {
            record.DateUpdated = DateTime.UtcNow;
            db.Set<ProjectStakeholder>().Add(record);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(ToDo))]
        public IHttpActionResult AddToDo(ToDo record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            record.DateUpdated = DateTime.UtcNow;
            db.Set<ToDo>().Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        [HttpPost]
        [ResponseType(typeof(Account))]
        public IHttpActionResult DeleteAccount(int id)
        {
            var record = db.Set<Account>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            var record = db.Set<Contact>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpPost]
        [ResponseType(typeof(Cost))]
        public IHttpActionResult DeleteCost(int id)
        {
            var record = db.Set<Cost>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpPost]
        [ResponseType(typeof(CostConfiguration))]
        public IHttpActionResult DeleteCostConfiguration(int id)
        {
            var record = db.Set<CostConfiguration>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var record = db.Set<Employee>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpPost]
        [ResponseType(typeof(Project))]
        public IHttpActionResult DeleteProject(int id)
        {
            var record = db.Set<Project>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpDelete]
        [ResponseType(typeof(ProjectStakeholder))]
        public IHttpActionResult DeleteProjectStakeholder(int id)
        {
            var record = db.Set<ProjectStakeholder>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [HttpDelete]
        [ResponseType(typeof(ToDo))]
        public IHttpActionResult DeleteToDo(int id)
        {
            var record = db.Set<ToDo>().Find(id);
            if (record == null)
            {
                return NotFound();
            }

            record.DateDeleted = DateTime.UtcNow;
            db.SaveChanges();

            return Ok(record);
        }

        [ResponseType(typeof(Account))]
        public IHttpActionResult GetAccount(int id)
        {
            var record = db.Set<Account>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<Account> GetAccounts()
        {
            return db.Set<Account>().Where(x => x.DateDeleted == null);
        }

        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return identity.Set<ApplicationUser>();
        }

        [ResponseType(typeof(Contact))]
        public IHttpActionResult GetContact(int id)
        {
            var record = db.Set<Contact>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<Contact> GetContacts()
        {
            return db.Set<Contact>().Where(x => x.DateDeleted == null);
        }

        [ResponseType(typeof(Cost))]
        public IHttpActionResult GetCost(int id)
        {
            var record = db.Set<Cost>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [ResponseType(typeof(CostConfiguration))]
        public IHttpActionResult GetCostConfiguration(int id)
        {
            var record = db.Set<CostConfiguration>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<CostConfiguration> GetCostConfigurations()
        {
            return db.Set<CostConfiguration>().Where(x => x.DateDeleted == null);
        }

        public IQueryable<Cost> GetCosts()
        {
            return db.Set<Cost>().Where(x => x.DateDeleted == null);
        }

        public IQueryable<Option> GetDegreeOptions()
        {
            return this.GetOptions(OptionCategoryEnum.Degree);
        }

        public IQueryable<Option> GetDivisionOptions()
        {
            return this.GetOptions(OptionCategoryEnum.Division);
        }

        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            var record = db.Set<Employee>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<Employee> GetEmployees()
        {
            return db.Set<Employee>().Where(x => x.DateDeleted == null);
        }

        public IQueryable<Option> GetGenderOptions()
        {
            return this.GetOptions(OptionCategoryEnum.Gender);
        }

        public IQueryable<Option> GetIndustryOptions()
        {
            return this.GetOptions(OptionCategoryEnum.Industry);
        }

        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            var record = db.Set<Project>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<Project> GetProjects()
        {
            return db.Set<Project>().Where(x => x.DateDeleted == null);
        }

        [ResponseType(typeof(ProjectStakeholder))]
        public IHttpActionResult GetProjectStakeholder(int id)
        {
            var record = db.Set<ProjectStakeholder>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<ProjectStakeholder> GetProjectStakeholders()
        {
            return db.Set<ProjectStakeholder>().Where(x => x.DateDeleted == null);
        }

        public IQueryable<Option> GetProjectStatusOptions()
        {
            return this.GetOptions(OptionCategoryEnum.ProjectStatus);
        }

        public IQueryable<Option> GetProjectTypeOptions()
        {
            return this.GetOptions(OptionCategoryEnum.ProjectType);
        }

        public IQueryable<Option> GetResourceTypeOptions()
        {
            return this.GetOptions(OptionCategoryEnum.ResourceType);
        }

        public IQueryable<Option> GetRoleOptions()
        {
            return this.GetOptions(OptionCategoryEnum.Role);
        }

        public IQueryable<Option> GetSkillOptions()
        {
            return this.GetOptions(OptionCategoryEnum.Skill);
        }

        [ResponseType(typeof(ToDo))]
        public IHttpActionResult GetToDo(int id)
        {
            var record = db.Set<ToDo>().Find(id);
            if (record == null || record.DateDeleted != null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        public IQueryable<ToDo> GetToDoes()
        {
            return db.Set<ToDo>().Where(x => x.DateDeleted == null);
        }

        public IQueryable<Option> GetToDoStatusOptions()
        {
            return this.GetOptions(OptionCategoryEnum.TodoStatus);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateAccount(Account updated)
        {
            updated.DateUpdated = DateTime.UtcNow;
            db.Entry(updated).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateContact(Contact updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = db.Set<Contact>().Find(updated.Id);
            if (record == null || record.DateDeleted != null)
            {
                return BadRequest();
            }

            record.AccountId = updated.AccountId;
            record.Alias = updated.Alias;
            record.Birthday = updated.Birthday;
            record.Email = updated.Email;
            record.FirstName = updated.FirstName;
            record.Gender = updated.Gender;
            record.Hobby = updated.Hobby;
            record.LastName = updated.LastName;
            record.Mobile = updated.Mobile;
            record.SocialAccounts = updated.SocialAccounts;
            record.Title = updated.Title;
            record.DateUpdated = DateTime.UtcNow;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateCost(Cost updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = db.Set<Cost>().Find(updated.Id);
            if (record == null || record.DateDeleted != null)
            {
                return BadRequest();
            }

            record.ActualCost = updated.ActualCost;
            record.ActualHours = updated.ActualHours;
            record.Alias = updated.Alias;
            record.Comment = updated.Comment;
            record.CostConfigurationId = updated.CostConfigurationId;
            record.EmployeeId = updated.EmployeeId;
            record.ProjectId = updated.ProjectId;
            record.ResourceType = updated.ResourceType;
            record.DateUpdated = DateTime.UtcNow;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateCostConfiguration(CostConfiguration updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = db.Set<CostConfiguration>().Find(updated.Id);
            if (record == null || record.DateDeleted != null)
            {
                return BadRequest();
            }

            record.Name = updated.Name;
            record.Year = updated.Year;
            record.Month = updated.Month;
            record.PlannedHours = updated.PlannedHours;
            record.Rates = updated.Rates;
            record.Description = updated.Description;
            record.DateUpdated = DateTime.UtcNow;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateEmployee(Employee updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var record = db.Set<Employee>().Find(updated.Id);
            if (record == null || record.DateDeleted != null)
            {
                return BadRequest();
            }

            record.Division = updated.Division;
            record.BeginDate = updated.BeginDate;
            record.EndDate = updated.EndDate;
            record.Skills = updated.Skills;
            record.Comment = updated.Comment;
            record.DateUpdated = DateTime.UtcNow;
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateProject(Project updated)
        {
            updated.DateUpdated = DateTime.UtcNow;
            db.Entry(updated).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateProjectStakeholder(ProjectStakeholder updated)
        {
            updated.DateUpdated = DateTime.UtcNow;
            db.Entry(updated).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateToDo(ToDo updated)
        {
            updated.DateUpdated = DateTime.UtcNow;
            db.Entry(updated).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected IQueryable<Option> GetOptions(OptionCategoryEnum category)
        {
            return db.Set<Option>().Where(x => x.Category == category && x.DateDeleted == null)
                .OrderBy(x => x.Ordinal);
        }
    }
}