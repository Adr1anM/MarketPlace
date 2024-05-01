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

namespace MarketPlace.Application.App.Products.Commands
{
    public record UpdatePrudoct(int Id,string Title, string Description, int CategoryID, int AuthorId, int Quantity, decimal Price, DateTime CreatedDate) : IRequest<ProductDto>;
    public class UpdateProductHandler : IRequestHandler<UpdatePrudoct, ProductDto>
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
        public async Task<ProductDto> Handle(UpdatePrudoct request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Products.GetByIdAsync(request.Id);

            if (entity == null)
            {
                _logger.LogError($"No Product found with Id:{request.Id}");
                throw new Exception("There is not such product");
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _mapper.Map(request, entity);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<ProductDto>(entity);
            }
            catch (ArgumentNullException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;
            }
            catch (AutoMapperMappingException ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw;

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
