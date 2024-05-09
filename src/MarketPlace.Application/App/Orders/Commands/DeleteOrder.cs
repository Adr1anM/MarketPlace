using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Orders.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Orders.Delete
{
    public record DeleteOrder(int id) : IRequest<OrderDto>;
    public class DeleteOrderHandler : IRequestHandler<DeleteOrder, OrderDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public DeleteOrderHandler(IMapper mapper,IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;   
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<DeleteOrderHandler>();
        }

        public async Task<OrderDto> Handle(DeleteOrder request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Orders.GetByIdAsync(request.id);
            if (entity == null)
            {
                _logger.LogError("No order found with Id:{0}",request.id);
                throw new Exception("No such Order found");
            }


            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Orders.DeleteAsync(entity.Id);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return _mapper.Map<OrderDto>(entity);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
          
    }
}
