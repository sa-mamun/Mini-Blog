using BlogApp.Core.Entities;
using BlogApp.Core.Services.Articlee;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Models
{
    public class GetBlogModel
    {
        private readonly IArticleService _articleService = new ArticleService();
        private readonly ICategoryService _categoryService = new CategoryService();

        public IList<Article> GetBlogList()
        {
            var article = _articleService.ArticleList();

            var filteredList = new List<Article>();

            foreach(var item in article)
            {
                var category = _categoryService.GetCategoryById(item.Category.Id);

                if(!category.IsDeleted)
                {
                    item.Category = category;

                    filteredList.Add(item);
                }
            }

            return filteredList;
        }
    }
}