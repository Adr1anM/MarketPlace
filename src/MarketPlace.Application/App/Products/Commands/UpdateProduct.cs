using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.App.Products.Commands
{
    public record UpdateProduct(int Id,string Title, string Description, int CategoryId, int AuthorId, int Quantity, decimal Price, DateTime CreatedDate, IList<int> SubCategoryIds, IFormFile? ImageData) : ICommand<ProductDto>;
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IFileManager _fileManager;
        public UpdateProductHandler(IMapper mapper, IUnitOfWork unitofwork, ILoggerFactory loggerFactory, IFileManager fileManager)
        {
            _mapper = mapper;
            _unitOfWork = unitofwork;
            _logger = loggerFactory.CreateLogger<UpdateProductHandler>();
            _fileManager = fileManager;
        }
        public async Task<ProductDto> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products
              .GetByIdWithIncludeAsync(request.Id, p => p.ProductSubcategories);

            if (product == null)
            {
                _logger.LogError($"Entity of type '{typeof(Product).Name}' with ID '{request.Id}' not found.");
                throw new EntityNotFoundException(typeof(Product), request.Id);
            }

            var existingCategory = await _unitOfWork.GetGenericRepository<Category>().GetByIdAsync(request.CategoryId);

            if (existingCategory == null)
            {
                _logger.LogError($"Entity of type '{typeof(Category).Name}' with ID '{request.CategoryId}' not found.");
                throw new EntityNotFoundException(typeof(Category), request.CategoryId);
            }

            try
            {
                _mapper.Map(request, product);
                if(request.ImageData != null)
                {
                    product.ImageData = await _fileManager.ConvertToFileBytesAsync(request.ImageData); 
                }
                else
                {
                    product.ImageData = null;
                }

                var currentSubcategories = product.ProductSubcategories.Select(ps => ps.SubCategoryId).ToList();
                var subcategoriesToAdd = request.SubCategoryIds.Except(currentSubcategories).ToList();
                var subcategoriesToRemove = currentSubcategories.Except(request.SubCategoryIds).ToList();

                await _unitOfWork.SaveAsync(cancellationToken);

                foreach (var subcategoryId in subcategoriesToRemove)
                {
                    var productSubcategory = product.ProductSubcategories.FirstOrDefault(ps => ps.SubCategoryId == subcategoryId);
                    if (productSubcategory != null)
                    {
                        product.ProductSubcategories.Remove(productSubcategory);
                        await _unitOfWork.GetGenericRepository<ProductSubCategory>().DeleteAsync(productSubcategory.Id);
                    }
                }

                foreach (var subcategoryId in subcategoriesToAdd)
                {
                    var newProductSubcategory = new ProductSubCategory
                    {
                        ProductId = product.Id,
                        SubCategoryId = subcategoryId
                    };
                    product.ProductSubcategories.Add(newProductSubcategory);
                    await _unitOfWork.GetGenericRepository<ProductSubCategory>().AddAsync(newProductSubcategory);
                }
                return _mapper.Map<ProductDto>(product);

            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "An error occurred while mappinig: {Message}", ex.Message);
                throw;
            }
        }
    }
}
