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
using Microsoft.AspNetCore.Identity;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;

namespace MarketPlace.Application.Orders.Create
{
    public record CreateOrder(int CretedById, DateTime CreatedDate, int PromocodeId, int Quantity, string ShippingAdress,int StatusId, decimal TotalPrice) : ICommand<OrderDto>;
    public class CreateOrderHandler : IRequestHandler<CreateOrder, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        public CreateOrderHandler(UserManager<User> userManager, IUnitOfWork unitOfWork,IMapper mapper, ILoggerFactory loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<CreateOrderHandler>();
        }
        public async Task<OrderDto> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.CretedById.ToString());
            if (user == null)
            {
                _logger.LogError($"Entity of type '{typeof(User).Name}' with ID '{request.CretedById}' not found.");
                throw new EntityNotFoundException(typeof(User), request.CretedById);
            }

            var existingPromocode = await _unitOfWork.GetGenericRepository<Promocode>().GetByIdAsync(request.PromocodeId);
            if (existingPromocode == null)
            {
                _logger.LogError($"Entity of type '{typeof(Promocode).Name}' with ID '{request.PromocodeId}' not found.");
                throw new EntityNotFoundException(typeof(Promocode), request.PromocodeId);
            }

            var order = _mapper.Map<Order>(request);    

            var orderResult = await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync(cancellationToken);
            return _mapper.Map<OrderDto>(orderResult);        
        }
    }
}
