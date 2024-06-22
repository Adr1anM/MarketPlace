using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MarketPlace.Application.Abstractions.Services;

namespace MarketPlace.Application.App.Products.Commands
{
    public record CreateProduct(string Title, string Description, int CategoryID, IList<int> SubCategoryIds, int AuthorId, int Quantity, decimal Price, DateTime CreatedDate, IFormFile? ImageData) : ICommand<ProductDto>;

    public class CreateProductHandler : IRequestHandler<CreateProduct, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IFileManager _fileManager;

        public CreateProductHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IFileManager fileManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<CreateProductHandler>();
            _fileManager = fileManager;
        }
        public async Task<ProductDto> Handle(CreateProduct request, CancellationToken cancellationToken)
        {

            var existingCategory = await _unitOfWork.GetGenericRepository<Category>().GetByIdAsync(request.CategoryID);
                
            if (existingCategory == null)
            {
                _logger.LogError($"Entity of type '{typeof(Category).Name}' with ID '{request.CategoryID}' not found.");
                throw new EntityNotFoundException(typeof(Category), request.CategoryID);
            }

            var existingAuthor = await _unitOfWork.Authors.GetByIdAsync(request.AuthorId); 
            if(existingAuthor == null)
            {
                _logger.LogError($"Entity of type '{typeof(Author).Name}' with ID '{request.AuthorId}' not found.");
                throw new EntityNotFoundException(typeof(Author), request.AuthorId);               
            }

            var subcategories = await _unitOfWork.GetGenericRepository<SubCategory>()
                .FindByWhere(sc => request.SubCategoryIds.Contains(sc.Id));

            if (subcategories.Count != request.SubCategoryIds.Count)
            {
                _logger.LogError("One or more subcategories not found.");
                throw new EntityNotFoundException(typeof(SubCategory), request.SubCategoryIds[0]);
            }


            var product = _mapper.Map<Product>(request);
            product.ImageData = await _fileManager.ConvertToFileBytesAsync(request.ImageData);
            var returnedProduct = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync(cancellationToken);

            var productSubcategories = subcategories.Select(sc => new ProductSubCategory
            {
                SubCategoryId = sc.Id,
                ProductId = returnedProduct.Id,
                SubCategory = sc,
                Product = returnedProduct

            }).ToList();

            foreach (var productSubcategory in productSubcategories)
            {
                await _unitOfWork.GetGenericRepository<ProductSubCategory>().AddAsync(productSubcategory);
            }

            product.ProductSubcategories.AddRange(productSubcategories);
            return _mapper.Map<ProductDto>(product);
          

        }
    }
}
