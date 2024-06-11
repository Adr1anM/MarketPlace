using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Paints.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Products.Querries
{
    public record GetAllProductsByIdQuerry(int id) : IRequest<IEnumerable<ProductDto>>;
    public class GetAllProductsQuerryHandler : IRequestHandler<GetAllProductsByIdQuerry, IEnumerable<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllProductsQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsByIdQuerry request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllProductsWithAuthorsAndUsersAsync(request.id);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
