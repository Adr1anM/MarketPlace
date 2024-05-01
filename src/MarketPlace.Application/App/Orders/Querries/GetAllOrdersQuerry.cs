using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Orders.Querries
{
    public record GetAllOrderQuerry() : IRequest<IEnumerable<OrderDto>>;
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuerry, IEnumerable<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllOrdersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderQuerry request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}
