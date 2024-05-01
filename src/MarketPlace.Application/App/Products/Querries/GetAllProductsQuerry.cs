using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Paints.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Products.GetPagedResult
{
    public record GetAllProductsQuerry() : IRequest<IEnumerable<ProductDto>>;
    public class GetAllProductsQuerryHandler : IRequestHandler<GetAllProductsQuerry, IEnumerable<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProductsQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuerry request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
