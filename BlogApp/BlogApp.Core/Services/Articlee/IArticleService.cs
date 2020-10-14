using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Services.Articlee
{
    public interface IArticleService
    {
        void AddArticle(Article article);
        IList<Article> ArticleList();
        Article GetArticleById(int id);
        void UpdateArticle(Article article);
        void DeleteArticle(Article article);
    }
}
