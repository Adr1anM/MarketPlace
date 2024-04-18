using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByUserId(int userId);
    }
}
