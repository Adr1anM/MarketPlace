using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Orders.Responses;
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
    public record GetPagedOrdersQuerry(int PageNumber, int PageSize) : IRequest<IEnumerable<OrderDto>>;

    public class GetPagedOrdersQuerryHandler : IRequestHandler<GetPagedProductsQuerry, IEnumerable<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedOrdersQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetPagedProductsQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Products.GetPagedResult(request.PageNumber, request.PageSize);

            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }
    }
}
