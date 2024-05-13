using AutoMapper;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Domain.Models;
using MediatR;
using System;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;

namespace MarketPlace.Application.App.Authors.Commands
{
    public record DeleteAuthor(int id) : ICommand<AuthorDto>;
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
                _logger.LogError($"Entity of type '{typeof(Author).Name}' with ID '{request.id}' not found.");
                throw new EntityNotFoundException(typeof(Author), request.id);
            }
            
            var result = await _unitOfWork.Authors.DeleteAsync(author.Id);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<AuthorDto>(result);  
            

        }
    }
}
