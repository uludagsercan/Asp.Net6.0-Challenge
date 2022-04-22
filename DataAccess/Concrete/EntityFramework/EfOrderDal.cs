using Core.DataAccess.EntityFramework.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal:GenericRepository<Order,ChallengeContext>, IOrderDal
    {
        public ICollection<Order> GetAllWithCustomer()
        {
            using (var context = new ChallengeContext())
            {
                var orders = context.Orders.Include(o => o.Customer).ToList();
                return orders;
            }
        }
    }
}
