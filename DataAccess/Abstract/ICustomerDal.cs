using Core.DataAccess.EntityFramework.Abstract;
using Entity.Concrete;
using Entity.Concrete.Dtos;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityFrameworkRepository<Customer>
    {
        ICollection<CustomerByNameDto> GetCustomerByNameDtos(string name);
        ICollection<CustomersWithOrderDto> GetCustomerIsNotContainsOrders();
    }
}
