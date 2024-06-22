using FluentValidation;
using MarketPlace.Application.App.Authors.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Authors.CommandValidators
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthor>
    {
        public UpdateAuthorCommandValidator()
        {

            RuleFor(a => a.Id)
               .GreaterThan(0)
               .NotEmpty();

            RuleFor(a => a.UserId)
               .GreaterThan(0)
               .NotEmpty();

            RuleFor(a => a.SocialMediaLinks)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(a => a.Country)
                .MaximumLength(150)
                .NotEmpty();

            RuleFor(a => a.Biography)
                .MaximumLength(400)
                .NotEmpty();

            RuleFor(a => a.BirthDate)
                .NotEmpty();
        }
    }
}
