using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.App.Login.AuthModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarketPlace.Application.App.Login.Commands
{
    public record RegisterCommand(RegisterModel RegisterData) : IRequest<string>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuthenticationService _authService;

        public RegisterCommandHandler(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request.RegisterData);
        }
    }
}
