using FluentValidation;
using MarketPlace.Application.Abstractions.Behaviors;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateProduct>();    
                cfg.AddOpenBehavior(typeof(ValidationBehaviors<,>)); 
            });

            services.AddValidatorsFromAssembly(assembly);



            return services;
        }
    }
}
