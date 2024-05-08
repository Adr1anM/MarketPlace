using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MediatR;  
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.Orders.Create
{
    public record CreateOrder(int CretedById, DateTime CreatedDate, int PromocodeId, int Quantity, string ShippingAdress,int StatusId, decimal TotalPrice) : IRequest<OrderDto>;
    public class CreateOrderHandler : IRequestHandler<CreateOrder, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CreateOrderHandler(IUnitOfWork unitOfWork,IMapper mapper, ILoggerFactory loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<CreateOrderHandler>();
        }
        public async Task<OrderDto> Handle(CreateOrder request, CancellationToken cancellationToken)
        {   
            var order = _mapper.Map<Order>(request);    
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var orderResult = await _unitOfWork.Orders.AddAsync(order);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<OrderDto>(orderResult);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
           
        }
    }
}
