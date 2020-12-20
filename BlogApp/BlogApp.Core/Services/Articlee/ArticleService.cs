using BlogApp.Core.Entities;
using BlogApp.Core.Exceptions;
using BlogApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Services.Articlee
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> _repository;

        public ArticleService(IRepository<Article> repository)
        {
            _repository = repository;
        }

        public void AddArticle(Article article)
        {
            var count = _repository.GetCount(x => x.Title == article.Title);

            if(count > 0)
            {
                throw new DuplicationException("Title Already Exists");
            }

            _repository.Insert(article);
        }

        public IList<Article> ArticleList()
        {
            return _repository.GetList();
        }

        public void DeleteArticle(Article article)
        {
            _repository.Delete(article);
        }

        public Article GetArticleById(int id)
        {
            return _repository.GetById(id);
        }

        public void UpdateArticle(Article article)
        {
            var count = _repository.GetCount(x => x.Title == article.Title && x.Id != article.Id);

            if (count > 0)
            {
                throw new DuplicationException("Title Already Exists");
            }

            _repository.Update(article);
        }
    }
}
