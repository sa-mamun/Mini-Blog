using Autofac;
using BlogApp.Core.Entities;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services.Articlee;
using BlogApp.Core.Services.Categoryy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core
{
    public class FrameworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryService>().As<ICategoryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ArticleService>().As<IArticleService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Repository<Category>>().As<IRepository<Category>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Repository<Article>>().As<IRepository<Article>>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
