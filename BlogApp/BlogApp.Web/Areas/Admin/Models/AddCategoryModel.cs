using BlogApp.Core.Entities;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Areas.Admin.Models
{
    public class AddCategoryModel
    {
        public virtual int? Id { get; set; }
        [Required(ErrorMessage = "Please Enter Category")]
        [DisplayName("Name")]
        public virtual String Name { get; set; }
        public virtual IList<Article> Articles { get; set; }

        private readonly ICategoryService _categoryService = new CategoryService();

        public void AddCategory()
        {
            var category = new Category
            {
                Name = this.Name
            };

            _categoryService.AddCategory(category);
        }

        public void EditCategory()
        {
            var category = new Category
            {
                Id = this.Id.Value,
                Name = this.Name,
                UpdatedAt = DateTime.Now
            };

            _categoryService.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            _categoryService.DeleteCategory(category);
        }
    }
}