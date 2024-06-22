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

namespace MarketPlace.Application.App.ShoppingCarts.Querries
{
    public record GetCatByUserIdQuery(int userId) : ICommand<ShoppingCartDto>;

    public class GetCatByUserIdQueryHandler : IRequestHandler<GetCatByUserIdQuery, ShoppingCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetCatByUserIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetCatByUserIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCatByUserIdQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ShoppingCartDto> Handle(GetCatByUserIdQuery request, CancellationToken cancellationToken)
        {
            var cartWithProduct = await _unitOfWork.ShoppingCarts.GetCartByUserIdWithIncludeAsync(request.userId);
            if (cartWithProduct == null)
            {
                _logger.LogError("Could not find shopping cart");
                throw new EntityNotFoundException(typeof(Domain.Models.ShoppingCart), request.userId);
            }
            return _mapper.Map<ShoppingCartDto>(cartWithProduct);
        }
    }
    
}
