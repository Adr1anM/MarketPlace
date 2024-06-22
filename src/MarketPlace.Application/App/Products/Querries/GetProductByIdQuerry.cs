using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Paints.Responses;
using MediatR;


namespace MarketPlace.Application.App.Products.GetPagedResult
{
    public record GetProductByIdQuerry(int Id) : IRequest<ProductDto>;
    public class GetOrderByIdHandler : IRequestHandler<GetProductByIdQuerry, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetOrderByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuerry request, CancellationToken cancellationToken)
        {
            var orderResult = await _unitOfWork.Products.GetByIdWithIncludeAsync(request.Id, 
                product => product.Author, p => p.Author.User, p => p.ProductSubcategories);
            return _mapper.Map<ProductDto>(orderResult);
        }
    }
}
