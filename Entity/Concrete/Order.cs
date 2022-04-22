
using System.Text.Json.Serialization;
using Core.DataAccess.Entity;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;


namespace Entity.Concrete
{
    public class Order:IEntity
    {
        public int OrderId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public int CustomerId { get; set; }
        
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingDefault)]
        public virtual Customer? Customer { get; set; }
    }
}
