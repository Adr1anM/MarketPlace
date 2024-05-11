using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Products.Commands
{
    public record CreateProduct(string Title, string Description, int CategoryID, int AuthorId, int Quantity, decimal Price, DateTime CreatedDate) : IRequest<ProductDto>;

    public class CreateProductHandler : IRequestHandler<CreateProduct, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public CreateProductHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<CreateProductHandler>();
        
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

            var product = _mapper.Map<Product>(request);
                  
            var returnedProduct = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync(cancellationToken);  
            return _mapper.Map<ProductDto>(returnedProduct);
          

        }
    }
}
