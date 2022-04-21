using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(true, data)
        {
        }

        public SuccessDataResult(string message) : base(true, message)
        {
        }

        public SuccessDataResult() : base(true)
        {
        }

        public SuccessDataResult(T data, string message) : base(true, data, message)
        {
        }
    }
}
