
using AutoMapper;
using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.App.ShoppingCarts.Responses;
using MarketPlace.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.App.ShoppingCart.Commands
{
    public record RemoveFromCart(int userId, int productId) : ICommand<ShoppingCartDto>;

    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCart, ShoppingCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveFromCartHandler> _logger;
        private readonly IMapper _mapper;

        public RemoveFromCartHandler(IUnitOfWork unitOfWork, ILogger<RemoveFromCartHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ShoppingCartDto> Handle(RemoveFromCart request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.ShoppingCarts.RemoveFromCartAsync(request.userId, request.productId);
            if (result == null)
            {
                _logger.LogError("Could not remove shopping cart");
                throw new EntityNotFoundException(typeof(Domain.Models.ShoppingCart), request.userId);
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            var cartWithProduct = await _unitOfWork.ShoppingCarts.GetCartByUserIdWithIncludeAsync(request.userId);
            return _mapper.Map<ShoppingCartDto>(cartWithProduct);

        }
    }
    
}
