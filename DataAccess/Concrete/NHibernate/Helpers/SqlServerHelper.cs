using Core.DataAccess.NHibernate;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.NHibernate.Helpers
{
    public class SqlServerHelper : NHibernateHelper
    {
        protected override ISessionFactory InitiliazeFactory()
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(c=> c.Server(@"DESKTOP-0MA5SJ1\SQLEXPRESS").
                Database("Challenge").TrustedConnection())).
                Mappings(m=> m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).
                BuildSessionFactory();
        }
    }
}
