using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.PostSharp.CacheAspects;
using Core.Aspects.PostSharp.LogAspects;
using Core.Aspects.PostSharp.TransactionAspects;
using Core.Aspects.PostSharp.ValidationAspects.FluentValidation;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Concrete.Dtos;
using System.Transactions;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        
        [FluentValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public IResult Add(Customer customer)
        {
            if (customer.CustomerId!=0)
            {
                return new ErrorResult("Müşteri id alanı boş geçilmelidir.");
            }
            _customerDal.Add(customer);
            return new SuccessResult("Müşteri kaydı oluşturuldu.");
        }

        public IResult Delete(int id)
        {
            var businessResult = BusinessRule.Run(CheckIfCustomerExist(id));
            if (businessResult!=null)
            {
                return businessResult;
            }
            var customer = _customerDal.Get(x => x.CustomerId==id);
            _customerDal.Delete(customer);
            return new SuccessResult("Silme işlemi başarılıdır.");
        }
        [LogAspects(typeof(DatabaseLogger))]
        [LogAspects(typeof(FileLogger))]
        public IDataResult<ICollection<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<ICollection<Customer>>(result,"Müşteriler listelendi");
        }

        public IDataResult<Customer> GetById(int id)
        {
            var businessResult = BusinessRule.Run(CheckIfCustomerExist(id));
            if (businessResult!=null)
            {
                return new ErrorDataResult<Customer>(businessResult.Message);
            }
            var result = _customerDal.Get(x => x.CustomerId == id);
            return new SuccessDataResult<Customer>(result,"Müşteri bulundu");
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public IDataResult<ICollection<CustomerByNameDto>> GetCustomerByNameDtos(string name)
        {
            
            var result = _customerDal.GetCustomerByNameDtos(name);
            if (result.Count()>0)
            {
                return new SuccessDataResult<ICollection<CustomerByNameDto>>(result,"Sipariş bilgisi olan müşteri listelendi.");
            }
            return new ErrorDataResult<ICollection<CustomerByNameDto>>("Sipariş bilgisine sahip müşteri kaydı bulunamadı");
        }

        public IDataResult<ICollection<CustomersWithOrderDto>> GetCustomerIsNotContainsOrders()
        {
            var result = _customerDal.GetCustomerIsNotContainsOrders();
            if (result.Count>0)
            {
                return new SuccessDataResult<ICollection<CustomersWithOrderDto>>(result,"Sipariş bilgisi olmayan müşteriler listelendi.");
            }
            return new ErrorDataResult<ICollection<CustomersWithOrderDto>>("Tüm müşterilerin sipariş bilgisi mevcuttur.");
        }

        //Transaction kullanımına örnek olarak oluşturuldu.
      
        public void TransactionOperation(Customer c1, Customer c2)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _customerDal.Add(c1);
                    _customerDal.Update(c2);
                    scope.Complete();
                }
                catch
                {
                    scope.Dispose();
                }
            }
        }
        [TransactionScopeAspect]
        public void TransactionOperation2(Customer c1, Customer c2)
        {
            _customerDal.Add(c1);
            _customerDal.Update(c2);
        }

        public IResult Update(Customer customer)
        {
            var businessResult = BusinessRule.Run(CheckIfCustomerExist(customer.CustomerId));
            if (businessResult!=null)
            {
                return businessResult;
            }
            _customerDal.Update(customer);
            return new SuccessResult("Güncelleme işlemi başarılıdır.");
        }

        private IResult CheckIfCustomerExist(int id)
        {
            var result = _customerDal.Get(x => x.CustomerId == id);
            if (result == null)
            {
                return new ErrorResult("Müşteri Bulunamadı");
            }
            return new SuccessResult();
        }
      
    }
}
