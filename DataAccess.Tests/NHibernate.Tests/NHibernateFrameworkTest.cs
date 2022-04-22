using DataAccess.Concrete.NHibernate;
using DataAccess.Concrete.NHibernate.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataAccess.Tests.NHibernate.Tests
{
    [TestClass]
    public class NHibernateFrameworkTest
    {
        [TestMethod]
        public void Get_all_returns_all_customers()
        {
            NhCustoemerDal nhCustoemerDal = new NhCustoemerDal(new SqlServerHelper());
            var result = nhCustoemerDal.GetAll();
            Assert.AreEqual(18, result.Count);
        }
    }
}