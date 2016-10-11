using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using api.Dtos;
using api.Jwt;
 
namespace api.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenProviderOptions _options;
        public TokenService(IOptions<TokenProviderOptions> options)
        {
            _options = options.Value;
        }
 
        public string GenerateToken(UserDto user)
        {
            // TODO: check valid credentials
        
            var now = DateTime.UtcNow;
        
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };
        
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials
                );
        
            return JsonConvert.SerializeObject(new
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(jwt),
                expires_in = (int)_options.Expiration.TotalSeconds
            });
        }

    }
}