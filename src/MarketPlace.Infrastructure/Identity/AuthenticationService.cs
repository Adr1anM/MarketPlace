using Microsoft.AspNetCore.Identity;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Application.Abstractions.Services;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.App.Login.AuthModels;

namespace MarketPlace.Infrastructure.Identity
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IJwtProvider _jwtProvider;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, ILogger<AuthenticationService> logger, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> RegisterAsync(RegisterModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User with Email {user.Email} registered successfully");
                return _jwtProvider.GenerateJwtToken(user.Id, user.Email);
            }
            else
            {
                _logger.LogError($"Unable to regiter the user with Email {user.Email}");
                return string.Join(", ", result.Errors);
            }
        }

        public async Task<string> SignInAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception("Sign-in failed. User not found.");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (isPasswordCorrect)
            {
                return _jwtProvider.GenerateJwtToken(user.Id, user.Email);
            }
            else
            {
                throw new Exception("Sign-in failed. Invalid password.");
            }
        }
    }
}
