using AutoMapper;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;

namespace MarketPlace.Application.App.Authors.Commands
{
    public record DeleteAuthor(int id) : IRequest<AuthorDto>;
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public DeleteAuthorHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = loggerFactory.CreateLogger<DeleteAuthorHandler>();    
        }

        public async Task<AuthorDto> Handle(DeleteAuthor request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(request.id);

            if (author == null)
            {
                _logger.LogError($"No such order with Id:{request.id} found");
                throw new Exception("No such order found");
            }


            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await _unitOfWork.Authors.DeleteAsync(author.Id);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<AuthorDto>(result);  
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
