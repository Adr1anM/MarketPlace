using FluentValidation;
using MarketPlace.Application.Orders.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Orders.CommandValidators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrder>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(o => o.CretedById)
               .GreaterThan(0)
               .NotEmpty();

            RuleFor(o => o.CreatedDate)
                .NotEmpty();

            RuleFor(o => o.Quantity)
                .GreaterThan(0)
                .NotEmpty();
                
            RuleFor(o => o.ShippingAdress)
                .NotEmpty();

            RuleFor(o => o.StatusId)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(o => o.TotalPrice)
               .GreaterThan(0)
               .NotEmpty();

            RuleFor(o => o.PromocodeId)
                .GreaterThan(0)
                .NotEmpty();
        }
    }
}
