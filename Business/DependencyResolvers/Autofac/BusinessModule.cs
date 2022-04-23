using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.EntityFramework.Abstract;
using Core.DataAccess.EntityFramework.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.DependencyResolvers.Autofac
{
    public class BusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ICustomerService>().As<CustomerManager>().SingleInstance();
            builder.RegisterType<ICustomerDal>().As<EfCustomerDal>().SingleInstance();

            builder.RegisterType<IOrderService>().As<OrderManager>().SingleInstance();
            builder.RegisterType<IOrderDal>().As<EfOrderDal>().SingleInstance();

            builder.RegisterType(typeof(IQueryableRepository<>)).As(typeof(EfQueryableRepository<>));
            builder.RegisterType<DbContext>().As<ChallengeContext>();
        }
    }
}
