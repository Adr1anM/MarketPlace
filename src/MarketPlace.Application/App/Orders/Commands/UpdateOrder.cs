using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Orders.Update
{
    public record UpdateOrder(int Id,int CretedById, DateTime CreatedDate, int PromocodeId, int Quantity, string ShippingAdress, int StatusId, decimal TotalPrice) : ICommand<OrderDto>;
    public class UpdateOrderHandler : IRequestHandler<UpdateOrder,OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UpdateOrderHandler(IMapper mapper,IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;   
            _unitOfWork = unitOfWork;  
            _logger = loggerFactory.CreateLogger<UpdateOrderHandler>();
        }
        public async Task<OrderDto> Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Orders.GetByIdAsync(request.Id);
 
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                _mapper.Map(request, entity);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<OrderDto>(entity);

            }
            catch(ArgumentNullException ex)
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
