using DataAccess.Concrete.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataAccess.Tests.EntityFramework.Tests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void Get_all_returns_all_customers()
        {
            EfCustomerDal customerDal = new EfCustomerDal();
            var result = customerDal.GetAll();
            Assert.AreEqual(18, result.Count);
        }
    }
}