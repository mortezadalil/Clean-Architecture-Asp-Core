using Cms.Core.Commands;
using Cms.Core.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task Add(string requestEmail, byte[] passwordHash, byte[] passwordSalt);
        Task<User> GetUser(string requestEmail);
    }
}
