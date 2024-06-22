using AutoMapper;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Users.Responses;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Models.Auth;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Users.Queries
{
    public record GetUserFromTokenQuery() : ICommand<UserDto>;
    public class GetUserFromTokenQueryHandler : IRequestHandler<GetUserFromTokenQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        public GetUserFromTokenQueryHandler(IMapper mapper, IUserService userService, ILogger<GetUserByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }
        public async Task<UserDto> Handle(GetUserFromTokenQuery request, CancellationToken cancellationToken)
        {
            var id = await _userService.GetUserIdFromToken();
            var normalizedId = Int32.Parse(id);
            var result = await _userService.GetByIdAsync(normalizedId);

            if (result == null)
            {
                _logger.LogError($"No such user with Id: {normalizedId}");
                throw new EntityNotFoundException(typeof(User), normalizedId);
            }
            var roles = await _userService.GetUserRoles(normalizedId);

            var userDto = _mapper.Map<UserDto>(result);
            userDto.Roles = roles;

            return userDto;
        }
    }
}
