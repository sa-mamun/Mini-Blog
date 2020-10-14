using BlogApp.Core.Entities;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services.Categoryy;
using BlogApp.Web.Areas.Admin.Models;
using BlogApp.Web.Models;
using BlogApp.Web.SeedHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        //#region Identity Config
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public CategoryController()
        //{
        //}

        //public CategoryController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}
        //#endregion

        // GET: Admin/Category
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var categoryModel = new GetCategoryModel();
            var articleModel = new GetArticleModel();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            //Total Users
            var userRole = roleManager.FindByName(RoleNames.ROLE_MEMBER);
            var totalUsers = context.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(userRole.Id))
                .ToList().Count();

            //Total Admins
            var role = roleManager.FindByName(RoleNames.ROLE_ADMIN);
            var totalAdmins = context.Users
                .Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id))
                .ToList().Count();

            //Total Category
            var totalCategory = categoryModel.GetCategoryCount();

            //Total Articles
            var totalArticles = articleModel.GetArticleCount();

            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalAdmins = totalAdmins;
            ViewBag.TotalCategory = totalCategory;
            ViewBag.TotalArticle = totalArticles;

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new AddCategoryModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(model.Id == null)
                    {
                        model.AddCategory();
                    }
                    else
                    {
                        model.EditCategory();
                    }
                    
                    return RedirectToAction("Create");
                }
                catch (DuplicationException ex)
                {
                    ViewBag.Message = ex.Message;
                    ModelState.AddModelError("", "Category Already Exists");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Process Failed");
                }
            }
            return View(model);
        }

        public ActionResult GetCategory()
        {
            var model = new GetCategoryModel();
            var categoryList = model.GetCategory();

            return Json(new { data = categoryList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCategoryById(int id)
        {
            var model = new GetCategoryModel();
            var result = model.GetCategoryById(id);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if(ModelState.IsValid)
            {
                var model = new AddCategoryModel();
                model.DeleteCategory(id);
            }

            return RedirectToAction("Create");
        }
    }
}