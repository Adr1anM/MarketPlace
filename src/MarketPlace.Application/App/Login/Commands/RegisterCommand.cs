using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Login.AuthModels;
using MarketPlace.Application.Exceptions;
using MediatR;


namespace MarketPlace.Application.App.Login.Commands
{
    public record RegisterCommand(RegisterModel RegisterData) : IRequest<string>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserService _userService;

        public RegisterCommandHandler(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool ifDuplicate = await _userService.CheckIfDuplicateUserName(request.RegisterData.FirstName, request.RegisterData.LastName);
            if (ifDuplicate)
            {
                throw new DuplicateUserNameException(request.RegisterData.FirstName, request.RegisterData.LastName);
            }
            return await _authService.RegisterAsync(request.RegisterData);
        }
    }
}
