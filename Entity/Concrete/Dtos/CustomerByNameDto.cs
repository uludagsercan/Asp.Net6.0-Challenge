using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete.Dtos
{
    public class CustomerByNameDto:IDto
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerAge { get; set; }
    }
}
