using BlogApp.Web.Areas.Admin.Models;
using BlogApp.Web.Common;
using BlogApp.Web.Models;
using BlogApp.Web.SeedHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagerController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManagerController()
        {
        }

        public ManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AddUserModel();
            return View(model);
        }

        #region Admin

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddUserModel model)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, ConstantValue.USER_DEFAULTPASSWORD);

                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore);

                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    userManager.AddToRole(user.Id, RoleNames.ROLE_ADMIN);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Create");
                    }
                }
            }
            return View(model);
        }

        public ActionResult GetAdminUser()
        {
            var context = new ApplicationDbContext();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = roleManager.FindByName(RoleNames.ROLE_ADMIN);
            var users = context.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id))
                .ToList();

            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region User

        [HttpGet]
        public ActionResult GetUser()
        {
            return View();
        }

        public ActionResult GetUserInfo()
        {
            var context = new ApplicationDbContext();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = roleManager.FindByName(RoleNames.ROLE_MEMBER);
            var users = context.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id))
                .ToList();

            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> EditUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var model = new EditUserModel();

            if(user != null)
            {
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.Id = user.Id;
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);

            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await UserManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                ViewBag.Message = "Updated Successfully";
                return RedirectToAction("GetUser");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var result = await UserManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                ViewBag.Message = "Deleted Successfully";
            }

            return RedirectToAction("GetUser");

        }

        #endregion
    }
}