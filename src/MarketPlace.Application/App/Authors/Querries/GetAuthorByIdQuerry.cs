using AutoMapper;
using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;



namespace MarketPlace.Application.App.Authors.Querries
{   
    public record GetAuthorByIdQuerry(int Id) : IRequest<AuthorDto>;
    public class GetAuthorByIdQuerryHandler : IRequestHandler<GetAuthorByIdQuerry, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAuthorByIdQuerryHandler> _logger;
        private readonly IFileManager _fileManager;

        public GetAuthorByIdQuerryHandler(IMapper mapper, IUnitOfWork unitOfWork, IFileManager fileManager, ILogger<GetAuthorByIdQuerryHandler> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileManager = fileManager;
            _logger = logger;
        }

        public async Task<AuthorDto> Handle(GetAuthorByIdQuerry request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetByIdWithIncludeAsync(request.Id, user => user.User);
            if(result == null )
            {
                _logger.LogError($"Entity of type {typeof(Author)} with id {request.Id} not found");
                throw new EntityNotFoundException(typeof(Author), request.Id);
            }

            var normalizedResult = _mapper.Map<AuthorDto>(result);

        
            return normalizedResult;
        }
    }
}
