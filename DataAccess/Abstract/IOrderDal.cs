using Core.DataAccess.EntityFramework.Abstract;
using Entity.Concrete;


namespace DataAccess.Abstract
{
    public interface IOrderDal: IEntityFrameworkRepository<Order>
    {
        public ICollection<Order> GetAllWithCustomer();
    }
}
