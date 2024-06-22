using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Common.Models;
using MarketPlace.Application.Paints.Responses;
using MediatR;
using MarketPlace.Application;


namespace MarketPlace.Application.Products.GetPagedResult
{
    public record GetPagedProductsQuerry(PagedRequest pagedRequest) : IRequest<PagedResult<ProductDto>>;
    public class GetPagedProductsHandler : IRequestHandler<GetPagedProductsQuerry, PagedResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedProductsHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResult<ProductDto>> Handle(GetPagedProductsQuerry request, CancellationToken cancellationToken)
        {
            return  await _unitOfWork.Products.GetPagedData<ProductDto>(request.pagedRequest, _mapper);

        }
    }
}
