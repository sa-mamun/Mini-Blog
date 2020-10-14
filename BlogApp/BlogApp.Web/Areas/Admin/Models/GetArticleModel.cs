using BlogApp.Core.Entities;
using BlogApp.Core.Services.Articlee;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Areas.Admin.Models
{
    public class GetArticleModel
    {
        private readonly ICategoryService _categoryService = new CategoryService();
        private readonly IArticleService _articleService = new ArticleService();

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