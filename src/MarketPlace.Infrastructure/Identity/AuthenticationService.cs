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
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if(userExists != null)
            {
                _logger.LogInformation($"User with Email {model.Email} already exists");
            }
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
            
                var defaultRole = model.Role;
                var roleExists = await _roleManager.RoleExistsAsync(defaultRole);
                if (!roleExists)
                {
                    _logger.LogError($"Role {defaultRole} does not exist.");
                    return $"Role {defaultRole} does not exist.";
                }

           
                var roleResult = await _userManager.AddToRoleAsync(user, defaultRole);
                if (!roleResult.Succeeded)
                {
                    _logger.LogError($"Unable to assign role to the user with Email {user.Email}");
                    return string.Join(", ", roleResult.Errors);
                }

                _logger.LogInformation($"User with Email {user.Email} registered successfully");

                var roles = await _userManager.GetRolesAsync(user);
                return _jwtProvider.GenerateJwtToken(user.Id, user.Email, roles);
            }
            else
            {
                _logger.LogError($"Unable to register the user with Email {user.Email}");
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
                var roles = await _userManager.GetRolesAsync(user);
                return _jwtProvider.GenerateJwtToken(user.Id, user.Email, roles);
            }
            else
            {
                throw new Exception("Sign-in failed. Invalid password.");
            }
        }
    }
}
