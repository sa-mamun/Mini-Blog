using BlogApp.Core.Entities;
using BlogApp.Core.Services.Articlee;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Areas.Admin.Models
{
    public class GetArticleModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public GetArticleModel()
        {
            _categoryService = DependencyResolver.Current.GetService<ICategoryService>();
            _articleService = DependencyResolver.Current.GetService<IArticleService>();
        }

        public GetArticleModel(ICategoryService categoryService,
            IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public IList<Category> GetAllCategory()
        {
            return _categoryService.CategoryList();
        }

        public IList<ModifiedArticle> GetCustomizeArticle()
        {
            var article = _articleService.ArticleList();

            ModifiedArticle modifiedArticle = null;
            var articleList = new List<ModifiedArticle>();

            foreach(var a in article)
            {
                var item = _categoryService.GetCategoryById(a.Category.Id);

                if(!item.IsDeleted)
                {
                    modifiedArticle = new ModifiedArticle
                    {
                        Id = a.Id,
                        Name = a.Title,
                        Category = item.Name
                    };

                    articleList.Add(modifiedArticle);
                }

            }

            return articleList;
        }

        public int GetArticleCount()
        {
            return _articleService.ArticleList().Count();

        }
    }
}