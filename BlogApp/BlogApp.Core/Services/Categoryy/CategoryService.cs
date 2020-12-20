using BlogApp.Core.Entities;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Services.Categoryy
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public void AddCategory(Category category)
        {
            var count = _repository.GetCount(x => x.Name == category.Name);

            if (count > 0)
            {
                throw new DuplicationException("Category Already Exists");
            }
            _repository.Insert(category);
        }

        public IList<Category> CategoryList()
        {
            return _repository.GetList(x => x.IsActive == true && x.IsDeleted == false);
        }

        public void DeleteCategory(Category category)
        {
            //Changing isActive and isDeleted Status
            category.IsActive = false;
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.Now;

            _repository.Update(category);
        }

        public Category GetCategoryById(int id)
        {
            return _repository.GetById(id);
        }

        public void UpdateCategory(Category category)
        {
            var count = _repository.GetCount(x => x.Name == category.Name && x.Id != category.Id);

            if (count > 0)
            {
                throw new DuplicationException("Category Already Exists");
            }
            _repository.Update(category);
        }
    }
}
