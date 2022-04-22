using Entity.Concrete;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.NHibernate.Mappings
{
    public class CustomerMap:ClassMap<Customer>
    {
        public CustomerMap()
        {
            Table(@"Customers");
            LazyLoad();
            Id(x => x.CustomerId).Column("CustomerId");
            Map(x => x.CustomerId).Column("CategoryId");
            Map(x => x.CustomerName).Column("CustomerName");
            Map(x => x.CustomerAge).Column("CustomerAge");
        }
    }
}
