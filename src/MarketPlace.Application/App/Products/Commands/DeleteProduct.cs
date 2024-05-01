using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Products.Delete
{
    public record DeleteProduct(int Id) : IRequest<ProductDto>;
    public class DeleteProductHandler : IRequestHandler<DeleteProduct, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DeleteProductHandler(IMapper mapper,IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<DeleteProductHandler>();
        }
        public async Task<ProductDto> Handle(DeleteProduct request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.Id);

            if(product == null)
            {
                _logger.LogError($"No product with Id:{request.Id}");
                throw new Exception("Product not Found");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Orders.DeleteAsync(product.Id);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<ProductDto>(product);    

            }
            catch(Exception ex) 
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);   
                throw;
            }
        }
    }
}
