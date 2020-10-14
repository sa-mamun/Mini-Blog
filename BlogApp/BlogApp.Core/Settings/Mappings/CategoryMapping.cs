using BlogApp.Core.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Settings.Mappings
{
    public class CategoryMapping : ClassMap<Category>
    {
        public CategoryMapping()
        {
            Table("tblCategory");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.CreatedAt).Nullable();
            Map(x => x.UpdatedAt).Nullable();
            Map(x => x.IsActive);
            Map(x => x.IsDeleted);
        }
    }
}
