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


namespace MarketPlace.Application.App.Authors.Commands
{
    public record CreateAuthor(int UserId,string Biography,string Country,DateTime BirthDate, string SocialMediaLinks,int NumberOfPosts) : IRequest<AuthorDto>;
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
                _logger.LogError($"User with Id:{request.UserId} not found");
                throw new Exception($"User with Id:{request.UserId} not found");
            }

            var author = _mapper.Map<Author>(request);      
            
            try
            {
                await _unitOfWork.BeginTransactionAsync();
    
                var result = await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<AuthorDto>(result);

            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
