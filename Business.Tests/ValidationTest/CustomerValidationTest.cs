using Business.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;


namespace Business.Tests.ValidationTest
{
    [TestClass]
    public class CustomerValidationTest
    {
        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void Customer_validation_tests()
        {
            Mock<ICustomerDal> mock = new Mock<ICustomerDal>();
            CustomerManager customerManager = new CustomerManager(mock.Object);
            customerManager.Add(new Customer{ CustomerName="Sercan"});
        }
    }
}