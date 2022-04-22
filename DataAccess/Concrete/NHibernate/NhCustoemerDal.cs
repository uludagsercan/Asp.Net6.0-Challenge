using Core.DataAccess.NHibernate;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.NHibernate
{
    public class NhCustoemerDal : NhEntityRepositoryBase<Customer>, ICustomerDal
    {
        public NhCustoemerDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }

        public ICollection<CustomerByNameDto> GetCustomerByNameDtos(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<CustomersWithOrderDto> GetCustomerIsNotContainsOrders()
        {
            throw new NotImplementedException();
        }
    }
}
