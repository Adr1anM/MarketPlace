using MarketPlace.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Extensions
{
    public static class ServiceExtentionCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           
            return services;
        }
    }
}
