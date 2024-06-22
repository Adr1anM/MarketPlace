using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Services;
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
        private readonly IFileManager _fileManager;
        public GetAllProductsQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileManager fileManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsByIdQuerry request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.Products.GetAllProductsWithAuthorsAndUsersAsync(request.id);
            /* var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

             foreach (var item in productDtos)
             {
                 var product = products.First(p => p.Id == item.Id);
                 if (product.ImageData != null)
                 {
                     item.ImageData =  _fileManager.ConvertToBase64String(product.ImageData);
                 }
             }
             return productDtos;*/  
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}
