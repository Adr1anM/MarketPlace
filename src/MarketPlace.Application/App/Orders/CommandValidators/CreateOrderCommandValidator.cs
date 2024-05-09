using FluentValidation;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Orders.CommandValidators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrder>
    {
        public CreateOrderCommandValidator()
        {

            RuleFor(o => o.CretedById)
                .NotEmpty();

            RuleFor(o => o.CreatedDate)
                .NotEmpty();

            RuleFor(o => o.Quantity)
                .NotEmpty();

            RuleFor(o => o.ShippingAdress)
                .NotEmpty();

            RuleFor(o => o.StatusId)
                .NotEmpty();

            RuleFor(o => o.TotalPrice)
               .NotEmpty();

        }
    }
}
