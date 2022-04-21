using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entity.Concrete;
using Entity.Concrete.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : GenericRepository<Customer, ChallengeContext>, ICustomerDal
    {
        public ICollection<CustomerByNameDto> GetCustomerByNameDtos(string name)
        {
            using (var context = new ChallengeContext())
            {
                var result = (from c in context.Customers
                              join o in context.Orders
                              on c.CustomerId equals o.CustomerId
                              where c.CustomerName.Contains(name)
                              select new CustomerByNameDto
                              {
                                  CustomerId = c.CustomerId,
                                  CustomerName = c.CustomerName,
                                  CustomerAge = c.CustomerAge,
                                  OrderId = o.OrderId
                              }).ToList();
                return result;
            }
        }

        public ICollection<CustomersWithOrderDto> GetCustomerIsNotContainsOrders()
        {
            using (var context = new ChallengeContext())
            {
                var result = (from c in context.Customers
                              join o in context.Orders
                              on c equals o.Customer into gj
                              from subcustomer in gj.DefaultIfEmpty()
                              where subcustomer == null
                              select new CustomersWithOrderDto
                              {
                                  CustomerId = c.CustomerId,
                                  CustomerAge = c.CustomerAge,
                                  CustomerName = c.CustomerName
                              }).ToList();
                return result;
            }
        }
    }
}
