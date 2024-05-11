using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Exceptions;
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
                _logger.LogError($"Entity of type '{typeof(Product).Name}' with ID '{request.Id}' not found.");
                throw new EntityNotFoundException(typeof(Product), request.Id);
            }

            var result =  await _unitOfWork.Products.DeleteAsync(product.Id);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<ProductDto>(result);    

        }
    }
}
