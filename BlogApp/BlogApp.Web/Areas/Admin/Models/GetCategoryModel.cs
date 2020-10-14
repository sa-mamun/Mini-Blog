using BlogApp.Core.Entities;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Areas.Admin.Models
{
    public class GetCategoryModel
    {
        private readonly ICategoryService _categoryService = new CategoryService();

        public IList<Category> GetCategory()
        {
            return _categoryService.CategoryList();
        }

        public Category GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }

        public int GetCategoryCount()
        {
            return this.GetCategory().Where(x => x.IsActive == true).Count();
        }
    }
}