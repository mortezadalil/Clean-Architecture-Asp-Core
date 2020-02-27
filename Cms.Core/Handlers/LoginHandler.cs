using Cms.Core.Commands;
using Cms.Core.Domains;
using Cms.Core.Interfaces.Repository;
using Cms.Core.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cms.Core.Commands.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cms.Core.Handlers
{
    public class LoginHandler : IRequestHandler<LoginInputQuery, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginInputQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Email);

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginResponse
                {
                    Token = string.Empty
                };
            }

            return new LoginResponse
            {
                Token = GenerateToken(user)
            };


        }


        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("Appsettings:SecretKey").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration.GetSection("Appsettings:ValidIssuer").Value,
                _configuration.GetSection("Appsettings:ValidAudience").Value,
                expires: DateTime.Now.AddHours(20),
                claims: new List<Claim>
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("Phone",user.Phone!=null ? user.Phone : "")
                }, signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

    }
}
