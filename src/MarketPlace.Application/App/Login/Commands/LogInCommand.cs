using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MarketPlace.Application.App.Login.AuthModels;
using MarketPlace.Domain.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarketPlace.Application.App.Login.Commands
{
    public record LogInCommand(string UserName, string Password) : ICommand<string>;

    public class LogInCommandHandler : IRequestHandler<LogInCommand, string>
    {
        private readonly IAuthenticationService _authenticationService;

        public LogInCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<string> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
           return await _authenticationService.SignInAsync(request.UserName, request.Password);
        }
    }
}
