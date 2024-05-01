using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Orders.Querries
{
    public record GetOrderByIdQuerry(int Id) : IRequest<OrderDto>;
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuerry, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }
        public async Task<OrderDto> Handle(GetOrderByIdQuerry request, CancellationToken cancellationToken)
        {
            var orderResult = await _unitOfWork.Orders.GetByIdWithIncludeAsync(request.Id, order => order.Buyer, order => order.ProductOrders);
            return _mapper.Map<OrderDto>(orderResult); 
        }
    }
}
