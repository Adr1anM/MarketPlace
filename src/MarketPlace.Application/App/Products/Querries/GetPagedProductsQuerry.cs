using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Paints.Responses;
using MediatR;


namespace MarketPlace.Application.Products.GetPagedResult
{
    public record GetPagedProductsQuerry(int PageNumber, int PageSize) : IRequest<IEnumerable<ProductDto>>;
    public class GetPagedProductsHandler : IRequestHandler<GetPagedProductsQuerry, IEnumerable<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedProductsHandler(IMapper mapper, IUnitOfWork unitOfWork)
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
