using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Identity
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateJwtToken(int id, string email, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, id.ToString()),
                new(JwtRegisteredClaimNames.Email, email)
            };

            // Add roles as claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
