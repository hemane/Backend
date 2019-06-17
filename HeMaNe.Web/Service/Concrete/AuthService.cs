using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HeMaNe.Shared.TransferObjects;
using HeMaNe.Web.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HeMaNe.Web.Service.Concrete
{
    internal class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSecret;

        public AuthService(IOptions<AppSettings> options, IUserService userService)
        {
            _appSecret = options.Value;
            _userService = userService;
        }

        public AuthDto Authenticate(UserDto userDto)
        {
            var user = this._userService.AuthUser(userDto);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSecret.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return new AuthDto
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}