using Core.Utilities.Results.Abstract;
using Entity.Concrete;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult Add(Order order);
        IResult Update(Order order);
        IResult Delete(int id);
        IDataResult<ICollection<Order>> GetAll();
        IDataResult<ICollection<Order>> GetAll(DateTime createdTime);
        IDataResult<Order> GetById(int id);
    }
}
