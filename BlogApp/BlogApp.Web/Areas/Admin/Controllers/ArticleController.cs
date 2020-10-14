using BlogApp.Core.Exceptions;
using BlogApp.Core.Services.Articlee;
using BlogApp.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        // GET: Admin/Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new GetArticleModel();
            var categoryList = model.GetAllCategory();
            ViewBag.Category = categoryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddArticleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.AddArticle();
                    return RedirectToAction("Index");
                }
                catch(DuplicationException ex)
                {
                    ViewBag.Message = ex.Message;
                    ModelState.AddModelError("", "Title Already Exists");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Save Failed");
                }
            }
            var articlemodel = new GetArticleModel();
            var categoryList = articlemodel.GetAllCategory();
            ViewBag.Category = categoryList;
            return View(model);
        }

        public ActionResult GetArticle()
        {
            var model = new GetArticleModel();
            var artileList = model.GetCustomizeArticle();

            return Json(new { data = artileList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new EditArticleModel();
            model.Load(id);
            var getArticleModel = new GetArticleModel();
            var categoryList = getArticleModel.GetAllCategory();
            ViewBag.Category = categoryList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditArticleModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.EditArticle();

                    return RedirectToAction("Index");
                }
                catch (DuplicationException ex)
                {
                    ViewBag.Message = ex.Message;
                    ModelState.AddModelError("", "Title Already Exists");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Update Failed");
                }
            }
            var getArticleModel = new GetArticleModel();
            var categoryList = getArticleModel.GetAllCategory();
            ViewBag.Category = categoryList;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var model = new EditArticleModel();
            model.DeleteArticle(id);

            return RedirectToAction("Index");
        }
    }
}