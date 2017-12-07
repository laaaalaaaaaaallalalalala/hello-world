using System.Collections.Generic;

namespace NorthernLights.CRM.Areas.Admin.Models.UsersViewModels
{
    public class CreateViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> Role { get; set; }

        public string UserName { get; set; }
    }

    public class EditViewModel
    {
        public string Email { get; set; }

        public int Id { get; set; }

        public IEnumerable<string> Role { get; set; }

        public string UserName { get; set; }
    }
}