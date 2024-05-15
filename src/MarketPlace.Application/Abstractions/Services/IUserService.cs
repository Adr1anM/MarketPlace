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
        Task<User?> GetById(int id);
        Task<User?> GetByEmailAsync(string email); 
    }
}
