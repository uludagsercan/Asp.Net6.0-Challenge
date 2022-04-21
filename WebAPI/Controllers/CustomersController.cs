using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : Controller
    {
        private CustomerManager cm = new CustomerManager(new EfCustomerDal());
        [HttpPost("add")]
        public IActionResult Add(Customer customer)
        {
            var result = cm.Add(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("delete")]

        public IActionResult Delete(int id)
        {
            var result = cm.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Customer customer)
        {
            var result = cm.Update(customer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = cm.GetAll();

            if (result.Success)
            {
                return Ok(cm.GetAll());
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = cm.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCustomerIsNotContainsOrders")]
        public IActionResult GetCustomerIsNotContainsOrders()
        {
            var result = cm.GetCustomerIsNotContainsOrders();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetCustomerByNameDtos")]
        public IActionResult GetCustomerByName(string name)
        {
            var result = cm.GetCustomerByNameDtos(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}