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
                .NotEmpty()
                .WithMessage("CretedById must be specified");

            RuleFor(o => o.CreatedDate)
                .NotEmpty();

            RuleFor(o => o.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(o => o.ShippingAdress)
                .NotEmpty()
                .WithMessage("ShippingAdress must be specified");

            RuleFor(o => o.StatusId)
                .NotEmpty();

            RuleFor(o => o.TotalPrice)
               .NotEmpty();

        }
    }
}
