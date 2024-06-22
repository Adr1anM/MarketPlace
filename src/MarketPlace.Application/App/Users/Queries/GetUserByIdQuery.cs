using AutoMapper;
using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Users.Responses;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Models.Auth;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Application.App.Users.Queries
{
    public record GetUserByIdQuery(int Id) : ICommand<UserDto>;
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        public GetUserByIdQueryHandler(IMapper mapper, IUserService userService, ILogger<GetUserByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(request.Id);
           
            if (result == null) 
            {
                _logger.LogError($"No such user with Id: {request.Id}");
                throw new EntityNotFoundException(typeof(User), request.Id);
            }
            var roles = await _userService.GetUserRoles(request.Id);

            var userDto = _mapper.Map<UserDto>(result);
            userDto.Roles = roles;
        
            return userDto;
        }
    }
 
}
