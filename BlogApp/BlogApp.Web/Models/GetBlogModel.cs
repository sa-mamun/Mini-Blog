using BlogApp.Core.Entities;
using BlogApp.Core.Services.Articlee;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Models
{
    public class GetBlogModel
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public GetBlogModel()
        {
            _categoryService = DependencyResolver.Current.GetService<ICategoryService>();
            _articleService = DependencyResolver.Current.GetService<IArticleService>();
        }

        public GetBlogModel(ICategoryService categoryService,
            IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

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