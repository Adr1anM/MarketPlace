using FluentValidation;
using MarketPlace.Application.App.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Products.ProductCommandValidator
{
    public class CreateProductComandValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductComandValidator()
        {
            
        }
    }
}
