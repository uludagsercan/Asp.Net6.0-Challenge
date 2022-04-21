using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;
        private ICustomerService _customerService;

        public OrderManager(IOrderDal orderDal, ICustomerService customerService)
        {

            _orderDal = orderDal;
            _customerService = customerService;
        }
        public IResult Add(Order order)
        {
            if (order.OrderId!=0)
            {
                return new ErrorResult("Sipariş id alanı boş geçilmelidir.");
            }
            var result = BusinessRule.Run(CheckIfCustomerExist(order.CustomerId));
            if (result!=null)
            {
                return new ErrorResult("Müşteri bilgisi bulunamadı");
            }
            _orderDal.Add(order);
            return new SuccessResult("Sipariş ekleme başarılıdır.");
        }

        public IResult Delete(int id)
        {
            var result = BusinessRule.Run(CheckIfOrderExist(id));
            if (result!=null)
            {
                return new ErrorResult("Sipariş bilgisi mevcut değildir.");
            }
            return new SuccessResult("Sipariş silindi");
        }

        public IDataResult<ICollection<Order>> GetAll()
        {
            var result = _orderDal.GetAllWithCustomer();
            return new SuccessDataResult<ICollection<Order>>(result, "Listeleme işlemi başarılıdır");
        }

        public IDataResult<ICollection<Order>> GetAll(DateTime createdTime)
        {
            var result = _orderDal.GetAll(x => x.CreateDate > createdTime);
            if (result.Count()>0)
            {
                return new SuccessDataResult<ICollection<Order>>(result,"Girilen tarihten sonraki siparişler listelenmiştir.");
            }
            return new ErrorDataResult<ICollection<Order>>("Girilen tarihten sonraki sipariş bilgisi bulunamamıştır.");
        }

        public IDataResult<Order> GetById(int id)
        {
            var businessResult = BusinessRule.Run(CheckIfOrderExist(id));
            if (businessResult!=null)
            {
                return new ErrorDataResult<Order>(businessResult.Message);
            }
            var result = _orderDal.Get(x => x.OrderId == id);
            return new SuccessDataResult<Order>(result,"Sipariş listeleme işlemi başarılıdır.");
            
        }

        public IResult Update(Order order)
        {
            var businessResult = BusinessRule.Run(CheckIfOrderExist(order.OrderId),
                CheckIfCustomerUpdated(order), CheckIfDateUpdated(order));
            if (businessResult!=null)
            {
                return businessResult;
            }
            _orderDal.Update(order);
            return new SuccessResult("Güncelleme işlemi başarılıdır.");
        }

        private IResult CheckIfCustomerExist(int id)
        {
            var customerResult = _customerService.GetById(id);
            if (!customerResult.Success)
            {
                return new ErrorResult("Müşteri bilgisi bulunamadı");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCustomerUpdated(Order order)
        {
            var result = _orderDal.Get(x=> x.OrderId == order.OrderId);
            if (result.CustomerId!=order.CustomerId)
            {
                return new ErrorResult("Müşteri bilgisi güncellenemez");
            }
            return new SuccessResult();
        }
        private IResult CheckIfOrderExist(int id)
        {
            var result = _orderDal.Get(x => x.OrderId == id);
            if (result==null)
            {
                return new ErrorResult("Sipariş bilgisi bulunamadı");
            }
            return new SuccessResult();
        }

        private IResult CheckIfDateUpdated(Order order)
        {
            var result = _orderDal.Get(x=> x.OrderId ==order.OrderId);
            if (result.CreateDate.Equals(order.CreateDate))
            {
                return new ErrorResult("Tarih bilgisi güncellenemez");
            }
            return new SuccessResult();
        }
    }
}