namespace NorthernLights.CRM.Migrations
{
    using NorthernLights.CRM.Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NorthernLights.CRM.Models.CrmDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "NorthernLights.CRM.Models.CrmDbContext";
        }

        protected override void Seed(NorthernLights.CRM.Models.CrmDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.ResourceType))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.ResourceType, Name = "Long-term", Description = "Long-term", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ResourceType, Name = "Short-term", Description = "Short-term", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ResourceType, Name = "Long-term/Half", Description = "Long-term/Half", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ResourceType, Name = "Long-term/Special", Description = "Long-term/Special", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ResourceType, Name = "Free", Description = "Free", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.Role))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Developer", Description = "Developer", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "QA", Description = "QA", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Graphic Designer", Description = "Graphic Designer", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Architect", Description = "Architect", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Scrum Master", Description = "Scrum Master", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Business Analyst", Description = "Business Analyst", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Team Lead", Description = "Team Lead", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Project Manager", Description = "Project Manager", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Role, Name = "Product Manager", Description = "Product Manager", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.Division))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.Division, Name = "United States", Description = "United States", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Division, Name = "China", Description = "China", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Division, Name = "India", Description = "India", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Division, Name = "Philippines", Description = "Philippines", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.Industry))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.Industry, Name = "Food", Description = "Food", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Industry, Name = "Printing", Description = "Printing", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Industry, Name = "Auto", Description = "Auto", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Industry, Name = "Insurance", Description = "Insurance", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.Skill))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "PHP", Description = "PHP", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "HTM5", Description = "HTM5", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "SQL Server", Description = "SQL Server", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "VB.NET", Description = "VB.NET", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "CSS3", Description = "CSS3", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "MySQL", Description = "MySQL", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "C#", Description = "C#", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "JavaScript", Description = "JavaScript", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "Oracle", Description = "Oracle", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Skill, Name = "Java", Description = "Java", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.Gender))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.Gender, Name = "Male", Description = "Male", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Gender, Name = "Female", Description = "Female", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.ProjectStatus))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.ProjectStatus, Name = "Started", Description = "Started", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ProjectStatus, Name = "No Started", Description = "No Started", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ProjectStatus, Name = "Finished", Description = "Finished", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ProjectStatus, Name = "Paused", Description = "Paused", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ProjectStatus, Name = "Cancelled", Description = "Cancelled", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.ProjectType))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.ProjectType, Name = "ITO", Description = "ITO", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ProjectType, Name = "BPO", Description = "BPO", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.ProjectType, Name = "Internal", Description = "Internal", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.Degree))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.Degree, Name = "High School", Description = "High School", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Degree, Name = "Associate", Description = "Associate", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Degree, Name = "Bachelor", Description = "Bachelor", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Degree, Name = "Master", Description = "Master", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Degree, Name = "Ph.D.", Description = "Ph.D.", Ordinal = 0, },
                    new Option() { Category = OptionCategoryEnum.Degree, Name = "M.B.A.", Description = "M.B.A.", Ordinal = 0, },
                });
            }

            if (!context.Set<Option>().Any(x => x.Category == OptionCategoryEnum.TodoStatus))
            {
                context.Set<Option>().AddOrUpdate(new Option[]
                {
                    new Option() { Category = OptionCategoryEnum.TodoStatus, Name = "Not Started", Description = "Not Started", Ordinal = 1, },
                    new Option() { Category = OptionCategoryEnum.TodoStatus, Name = "In Progress", Description = "In Progress", Ordinal = 2, },
                    new Option() { Category = OptionCategoryEnum.TodoStatus, Name = "Completed", Description = "Completed", Ordinal = 3, },
                    new Option() { Category = OptionCategoryEnum.TodoStatus, Name = "Deferred", Description = "Deferred", Ordinal = 4, },
                });
            }

            context.SaveChanges();
        }
    }
}