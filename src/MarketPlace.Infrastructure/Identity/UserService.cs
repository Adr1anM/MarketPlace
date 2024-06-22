using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MarketPlace.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManageer;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ArtMarketPlaceDbContext _context;

        public UserService(UserManager<User> userManageer, IHttpContextAccessor contextAccessor, ArtMarketPlaceDbContext context)
        {
            _userManageer = userManageer;
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManageer.FindByEmailAsync(email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userManageer.FindByIdAsync(id.ToString());
        }

        public async Task<IList<string>> GetUserRoles(int id)
        {
            var user = await GetByIdAsync(id);
            return await _userManageer.GetRolesAsync(user);

        }

        public async Task<string> GetUserIdFromToken()
        {
            var result = string.Empty;
            if(_contextAccessor.HttpContext is not null)
            {
                result = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            }
            return result;
        }


        public async Task<bool> CheckIfDuplicateUserName(string firstName, string lastName)
        {
            
            return await _context.Users.AnyAsync(u => u.FirstName == firstName && u.LastName == lastName);
         
        }

    }
}