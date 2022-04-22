using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CustomerName).NotNull().WithMessage("Müşteri ismi alanı boş geçilemez");
            RuleFor(x => x.CustomerAge).GreaterThan(15).WithMessage("Müşteri yaşı 15'ten büyük olmak zorundadır.");
            RuleFor(x => x.CustomerName).Must(StartsWithA);
        }
    private bool StartsWithA(string name)
        {
            if (name.StartsWith('A'))
            {
                return true;
            }
            return false;
        }
    }
}
