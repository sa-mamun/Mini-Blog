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
    public class EditArticleModel
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

        private readonly IArticleService _articleService = new ArticleService();
        private readonly ICategoryService _categoryService = new CategoryService();

        public void Load(int id)
        {
            var article = _articleService.GetArticleById(id);
            var category = _categoryService.GetCategoryById(article.Category.Id);

            if(article != null && category != null)
            {
                Id = article.Id;
                Title = article.Title;
                Description = article.Description;
                Category = category;
            }
        }

        public void EditArticle()
        {
            var article = new Article
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description
                
            };

            var category = _categoryService.GetCategoryById(this.CategoryId);
            article.Category = category;

            _articleService.UpdateArticle(article);
        }

        public void DeleteArticle(int id)
        {
            var article = _articleService.GetArticleById(id);
            _articleService.DeleteArticle(article);
        }
    }
}