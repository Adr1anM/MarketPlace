using FluentValidation;
using MarketPlace.Application.Abstractions.Behaviors;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.CommandValidators;
using MarketPlace.Application.App.Orders.CommandValidators;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.App.Products.CommandValidator;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Update;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MarketPlace.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly));
           

            services.AddTransient<IValidator<CreateOrder>, CreateOrderCommandValidator>();
            services.AddTransient<IValidator<UpdateOrder>, UpdateOrderCommandValidator>();
            services.AddTransient<IValidator<UpdateProduct>, UpdateProductCommandValidator>();
            services.AddTransient<IValidator<CreateProduct>, CreateProductCommandValidator>();
            services.AddTransient<IValidator<CreateAuthor>, CreateAuthorCommandValidator>();
            services.AddTransient<IValidator<UpdateAuthor>, UpdateAuthorCommandValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviors<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(TransactionBehavior<,>));
            return services;
        }
    }
}
