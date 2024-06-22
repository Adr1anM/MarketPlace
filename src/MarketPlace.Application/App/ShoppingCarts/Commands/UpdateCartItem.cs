using AutoMapper;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.ShoppingCarts.Responses;
using MarketPlace.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.App.ShoppingCarts.CartModels;

namespace MarketPlace.Application.App.ShoppingCart.Commands
{
    public record UpdateCartItem(AddCartModel model) : ICommand<ShoppingCartDto>;

    public class UpdateCartItemHandler : IRequestHandler<UpdateCartItem, ShoppingCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateCartItemHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateCartItemHandler(IUnitOfWork unitOfWork, ILogger<UpdateCartItemHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ShoppingCartDto> Handle(UpdateCartItem request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.ShoppingCarts.UpdateCartItemQuantityAsync(request.model.UserId, request.model.ProductId, request.model.Quantity);
            if (result == null)
            {
                _logger.LogError("Could not update shopping cart");
                throw new EntityNotFoundException(typeof(Domain.Models.ShoppingCart), request.model.UserId);
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            var cartWithProduct = await _unitOfWork.ShoppingCarts.GetCartByUserIdWithIncludeAsync(request.model.UserId);
            return _mapper.Map<ShoppingCartDto>(cartWithProduct);

        }
    }
}
