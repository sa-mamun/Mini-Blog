using BlogApp.Core.Settings.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Settings.DbConnection
{
    public class NHibernateSQLContext
    {
        private static ISessionFactory _session;

        private static ISessionFactory CreateSession()
        {
            if (_session != null)
            {
                return _session;
            }

            FluentConfiguration _config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    x => x.Server(@"DESKTOP-6OO9DT5\SHAMIM")
                    .Username("demo")
                    .Password("123456")
                    .Database("BlogTestOne")))

                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ArticleMapping>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CategoryMapping>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));

            _session = _config.BuildSessionFactory();

            return _session;
        }

        public static ISession SessionOpen()
        {
            return CreateSession().OpenSession();
        }
    }
}
