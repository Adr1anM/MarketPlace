using MarketPlace.Application.App.Login.AuthModels;
using MarketPlace.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<string> SignInAsync(string username, string password);
        Task<string> RegisterAsync(RegisterModel model);
    }
}
    