using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace MarketPlace.Application.App.Authors.Commands
{
    public record UpdateAuthor(int Id, int UserId, string Biography, string Country, DateTime BirthDate, string SocialMediaLinks, int NumberOfPosts,IFormFile? ProfileImage) : ICommand<AuthorDto>;
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IFileManager _fileManager;


        public UpdateAuthorHandler(IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger<UpdateAuthorHandler>();
            _fileManager = fileManager;
        }
        public async Task<AuthorDto> Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Authors.GetByIdAsync(request.Id);
            if(result == null)
            {
                _logger.LogError($"Entity of type '{typeof(Author).Name}' with ID '{request.Id}' not found.");
                throw new EntityNotFoundException(typeof(Author), request.Id);
            }

            try
            {
                _mapper.Map(request, result);
                if (request.ProfileImage != null)
                {
                    result.ProfileImage = await _fileManager.ConvertToFileBytesAsync(request.ProfileImage);
                }
                else
                {
                    result.ProfileImage = null;
                }

                await _unitOfWork.SaveAsync(cancellationToken);
                return _mapper.Map<AuthorDto>(result);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError(ex, "AutoMapperMappingException occurred: {Message}", ex.Message);
                throw;

            }       
        }
    }
}
