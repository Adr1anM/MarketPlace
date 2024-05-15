using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Abstractions.Services
{
    public interface IJwtProvider
    {
        string GenerateJwtToken(int Id, string Email);
    }
}
