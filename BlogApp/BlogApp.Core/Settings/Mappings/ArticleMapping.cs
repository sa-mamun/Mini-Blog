using BlogApp.Core.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Settings.Mappings
{
    public class ArticleMapping : ClassMap<Article>
    {
        public ArticleMapping()
        {
            Table("tblArticle");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Description).Length(20000);
            Map(x => x.CreatedAt).Nullable();
            Map(x => x.UpdatedAt).Nullable();
            Map(x => x.IsActive);
            Map(x => x.IsDeleted);

            References(x => x.Category)
                .Column("CategoryId").Not.Nullable();
        }
    }
}
