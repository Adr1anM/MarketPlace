﻿using FluentValidation;
using MarketPlace.Application.App.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Products.CommandValidator
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProduct>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(o => o.AuthorId)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(o => o.CreatedDate)
                .NotEmpty();

            RuleFor(o => o.Quantity)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(o => o.Price)
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(o => o.Title)
                .MinimumLength(0)
                .MaximumLength(250)
                .NotEmpty();

            RuleFor(o => o.Description)
               .MinimumLength(0)
               .MaximumLength(300)
               .NotEmpty(); 
            
            RuleFor(o => o.CategoryID)
               .GreaterThan(0)
               .NotEmpty();

        }
    }
}
