using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class DataResult<T>:Result,IDataResult<T>
    {
        public T Data { get; set; }
        
        public DataResult(bool success,T data, string message):this(success)
        {
            this.Data = data;
            this.Message = message;
        }

        public DataResult(bool success,T data)
        {
            this.Success=success;
            this.Data=data;
        }

        public DataResult(bool success,string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public DataResult(bool success)
        {
            this.Success = success;
        }
    }
}
