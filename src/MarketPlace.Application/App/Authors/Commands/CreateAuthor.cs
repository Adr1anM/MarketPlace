using AutoMapper;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;


namespace MarketPlace.Application.App.Authors.Commands
{
    public record CreateAuthor(int UserId,string Biography,string Country,DateTime BirthDate, string SocialMediaLinks,int NumberOfPosts, string PhoneNumber, IFormFile? ProfileImage) : ICommand<AuthorDto>;
    public class CreateAuthorHandler : IRequestHandler<CreateAuthor, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;   
        private readonly IFileManager _fileManager;
        public CreateAuthorHandler(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory, IFileManager fileManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<CreateAuthorHandler>();
            _fileManager = fileManager;
        }
        public async Task<AuthorDto> Handle(CreateAuthor request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if(user == null) 
            {
                _logger.LogError($"Entity of type '{typeof(Author).Name}' with ID '{request.UserId}' not found.");
                throw new EntityNotFoundException(typeof(Author), request.UserId);
            }

            var author = _mapper.Map<Author>(request);
            author.ProfileImage = await _fileManager.ConvertToFileBytesAsync(request.ProfileImage);


            var result = await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<AuthorDto>(result);
        }
    }
}
