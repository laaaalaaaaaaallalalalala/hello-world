using Microsoft.AspNet.Identity.Owin;
using NorthernLights.CRM.Areas.Admin.Models.UsersViewModels;
using NorthernLights.CRM.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NorthernLights.CRM.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private ApplicationRoleManager _roleManager;

        private ApplicationUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        [HttpPost]
        public JsonResult Create(CreateViewModel model)
        {
            if (UserManager.FindByEmailAsync(model.Email).Result != null)
            {
                return Json(new
                {
                    error = true,
                    message = "Email is in use. Please try a different one.",
                });
            }

            if (UserManager.FindByNameAsync(model.UserName).Result != null)
            {
                return Json(new
                {
                    error = true,
                    message = "User Name is in use. Please try a different one.",
                });
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = UserManager.CreateAsync(user, model.Password).Result;
            if (!result.Succeeded)
            {
                return Json(new
                {
                    error = true,
                    message = result.Errors.FirstOrDefault(),
                });
            }

            if (model.Role.Any(x => x != "false"))
            {
                result = UserManager.AddToRolesAsync(user.Id, model.Role.Where(x => x != "false").ToArray()).Result;
            }

            var code = UserManager.GenerateEmailConfirmationTokenAsync(user.Id).Result;
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { area = "", userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your <b>Northern Lights CRM</b> account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return Json(new
            {
                error = false,
                message = "A confirmation email has been sent.",
            });
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var user = UserManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                UserManager.DeleteAsync(user).Wait();
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id = id.Value;
            ViewBag.Roles = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        [HttpPut]
        public void Edit(EditViewModel model)
        {
            var userRoles = UserManager.GetRolesAsync(model.Id).Result;
            var selectedRoles = model.Role.Where(x => x != "false");
            UserManager.AddToRolesAsync(model.Id, selectedRoles.Except(userRoles).ToArray<string>()).Wait();
            UserManager.RemoveFromRolesAsync(model.Id, userRoles.Except(selectedRoles).ToArray<string>()).Wait();
        }

        public JsonResult GetUser(int id)
        {
            var roles = RoleManager.Roles.Select(x => new { x.Id, x.Name }).ToList();
            var user = UserManager.FindByIdAsync(id).Result;
            return Json(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = user.Roles.Select(y => roles.SingleOrDefault(z => z.Id == y.RoleId)).ToList(),
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers()
        {
            var roles = RoleManager.Roles.Select(x => new { x.Id, x.Name }).ToList();
            var data = UserManager.Users.ToList().Select(x => new
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Roles = x.Roles.Select(y => roles.SingleOrDefault(z => z.Id == y.RoleId)),
                });
            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}