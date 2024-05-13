using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
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
    public record UpdateProduct(int Id,string Title, string Description, int CategoryID, int AuthorId, int Quantity, decimal Price, DateTime CreatedDate) : ICommand<ProductDto>;
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public UpdateProductHandler(IMapper mapper, IUnitOfWork unitofwork, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitofwork;
            _logger = loggerFactory.CreateLogger<UpdateProductHandler>();
        }
        public async Task<ProductDto> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Products.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogError($"Entity of type '{typeof(Product).Name}' with ID '{request.Id}' not found.");
                throw new EntityNotFoundException(typeof(Product), request.Id);
            }

            try
            {
                _mapper.Map(request, entity);
                await _unitOfWork.SaveAsync(cancellationToken);
                return _mapper.Map<ProductDto>(entity);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "An error occurred while mappinig: {Message}", ex.Message);
                throw;
            }
        }
    }
}
