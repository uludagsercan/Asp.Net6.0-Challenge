using Core.DataAccess.EntityFramework;
using Entity.Concrete;


namespace DataAccess.Abstract
{
    public interface IOrderDal: IEntityFrameworkRepository<Order>
    {
        public ICollection<Order> GetAllWithCustomer();
    }
}
