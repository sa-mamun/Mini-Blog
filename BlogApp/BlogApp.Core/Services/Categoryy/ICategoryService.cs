using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Services.Categoryy
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        IList<Category> CategoryList();
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
