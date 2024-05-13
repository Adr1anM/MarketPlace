using FluentValidation;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommandBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await next();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return result;
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;   
            }   
        }
    }
}
