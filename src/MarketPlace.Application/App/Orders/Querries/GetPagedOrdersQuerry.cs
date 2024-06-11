using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Common.Models;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Products.GetPagedResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Orders.Querries
{
    public record GetPagedOrdersQuerry(PagedRequest pagedRequest) : IRequest<IEnumerable<OrderDto>>;

    public class GetPagedOrdersQuerryHandler : IRequestHandler<GetPagedOrdersQuerry, IEnumerable<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedOrdersQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<OrderDto>> Handle(GetPagedOrdersQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Products.GetPagedData<OrderDto>(request.pagedRequest,_mapper);

            return _mapper.Map<IEnumerable<OrderDto>>(result);
        }
    }
}
