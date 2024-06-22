using MarketPlace.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IList<string>> GetUserRoles(int id);
        Task<string> GetUserIdFromToken();
        Task<bool> CheckIfDuplicateUserName(string firstName, string lastName);
    }
}
