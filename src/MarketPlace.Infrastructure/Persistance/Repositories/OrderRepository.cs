using AutoMapper;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Common.Models;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ArtMarketPlaceDbContext context) : base(context)
        {

        }

        public async Task<Order> GetOrderByUserId(int userId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(p => p.CretedById == userId);

            if(order == null)
            {
                throw new ValidationException($"Object of type {typeof(Order)} with Buyier Id:{userId} not found");
        
            }
           

            return order;
        }
        public override Task<PagedResult<TDto>> GetPagedData<TDto>(PagedRequest pagedRequest, IMapper mapper) where TDto : class
        {
            return base.GetPagedData<TDto>(pagedRequest, mapper);
        }
    }
}
