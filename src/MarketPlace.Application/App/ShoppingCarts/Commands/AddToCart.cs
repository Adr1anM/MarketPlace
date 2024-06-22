using AutoMapper;
using MarketPlace.Application.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.App.ShoppingCarts.Responses;
using MarketPlace.Application.App.ShoppingCarts.CartModels;

namespace MarketPlace.Application.App.ShoppingCarts.Commands
{
    public record AddToCart(AddCartModel model) : ICommand<ShoppingCartDto>;

    public class AddToCartHandler : IRequestHandler<AddToCart, ShoppingCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AddToCartHandler(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<AddToCartHandler>();
            _mapper = mapper;
        }
        public async Task<ShoppingCartDto> Handle(AddToCart request, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.ShoppingCarts.AddToCartAsync(request.model.UserId, request.model.ProductId, request.model.Quantity);
            if (result == null)
            {
                _logger.LogError("Could not add to shopping cart");
                throw new EntityNotFoundException(typeof(Domain.Models.ShoppingCart), request.model.UserId);
            }

            await _unitOfWork.SaveAsync(cancellationToken);

            var cartWithProduct =  await _unitOfWork.ShoppingCarts.GetCartByUserIdWithIncludeAsync(request.model.UserId);  
            return _mapper.Map<ShoppingCartDto>(cartWithProduct);

        }
    }
}
