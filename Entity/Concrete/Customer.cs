using Entity.Abstract;

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Entity.Concrete
{

    public class Customer:IEntity
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerAge { get; set; }
   
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}
