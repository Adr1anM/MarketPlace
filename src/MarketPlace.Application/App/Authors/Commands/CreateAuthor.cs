using AutoMapper;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.Exceptions;
using System.Windows.Input;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;


namespace MarketPlace.Application.App.Authors.Commands
{
    public record CreateAuthor(int UserId,string Biography,string Country,DateTime BirthDate, string SocialMediaLinks,int NumberOfPosts) : ICommand<AuthorDto>;
    public class CreateAuthorHandler : IRequestHandler<CreateAuthor, AuthorDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;   
        public CreateAuthorHandler(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager; 
            _logger = loggerFactory.CreateLogger<CreateAuthorHandler>();
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
               
            var result = await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.SaveAsync(cancellationToken);

            return _mapper.Map<AuthorDto>(result);
        }
    }
}
