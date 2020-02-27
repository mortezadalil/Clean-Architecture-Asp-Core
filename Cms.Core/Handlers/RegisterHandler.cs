using Cms.Core.Commands;
using Cms.Core.Domains;
using Cms.Core.Interfaces.Repository;
using Cms.Core.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cms.Core.Commands.Register;

namespace Cms.Core.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterInputCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterInputCommand request, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
           
            var existUser=await _userRepository.GetUser(request.Email);

            if (existUser != null)
            {
                throw new Exception("User Already Exist");
            }
            await _userRepository.Add(request.Email, passwordHash, passwordSalt);

            return Unit.Value;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                 passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
