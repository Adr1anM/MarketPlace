﻿using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Infrastructure.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManageer;

        public UserService(UserManager<User> userManageer)
        {
            _userManageer = userManageer;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManageer.FindByEmailAsync(email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _userManageer.FindByIdAsync(id.ToString());
        }
    }
}
