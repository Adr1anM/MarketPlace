using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MediatR;
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

            var product = _mapper.Map<Product>(request);

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var returnedProduct = await _unitOfWork.Products.AddAsync(product);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<ProductDto>(returnedProduct);

            }
            catch (Exception ex)
            {          
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
        }
    }
}
