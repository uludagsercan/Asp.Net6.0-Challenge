using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Concrete.Dtos;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IResult Add(Customer customer);
        IDataResult<Customer> GetById(int id);
        IDataResult<ICollection<Customer>> GetAll();
        IDataResult<ICollection<CustomersWithOrderDto>> GetCustomerIsNotContainsOrders();
        IDataResult<ICollection<CustomerByNameDto>> GetCustomerByNameDtos(string name);
        IResult Delete(int id);
        IResult Update(Customer customer);

        void TransactionOperation(Customer c1, Customer c2);
        void TransactionOperation2(Customer c1, Customer c2);
    }
}
