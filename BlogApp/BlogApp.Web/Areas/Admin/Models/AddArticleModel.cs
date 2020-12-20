using BlogApp.Core.Entities;
using BlogApp.Core.Services.Articlee;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Areas.Admin.Models
{
    public class AddArticleModel
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Title")]
        [DisplayName("Title")]
        public virtual string Title { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        public virtual string Description { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [DisplayName("Catagory")]
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public IEnumerable<Category> items { get; set; }

        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public AddArticleModel()
        {
            _categoryService = DependencyResolver.Current.GetService<ICategoryService>();
            _articleService = DependencyResolver.Current.GetService<IArticleService>();
        }

        public AddArticleModel(ICategoryService categoryService,
            IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public void AddArticle()
        {
            var article = new Article
            {
                Title = this.Title,
                Description = this.Description,
                CategoryId = this.CategoryId
            };

            var category = _categoryService.GetCategoryById(this.CategoryId);
            article.Category = category;

            _articleService.AddArticle(article);
        }
    }
}