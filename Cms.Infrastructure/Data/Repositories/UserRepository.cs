using System.Linq;
using System.Threading.Tasks;
using Cms.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using UserEntity = Cms.Infrastructure.Data.Entities.User;
using UserDomain = Cms.Core.Domains.User;


namespace Cms.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public CmsDbContext _cmsDbContext { get; }

        public UserRepository(CmsDbContext cmsDbContext)
        {
            _cmsDbContext = cmsDbContext;
        }

        public async Task Add(string requestEmail, byte[] passwordHash, byte[] passwordSalt)
        {
            var item = new UserEntity
            {
                Email = requestEmail,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _cmsDbContext.Users.AddAsync(item);
            await _cmsDbContext.SaveChangesAsync();
        }

        public async Task<UserDomain> GetUser(string requestEmail)
        {
            return await _cmsDbContext.Users.Select(x => new UserDomain
            {
                Email=x.Email,
                Id=x.Id,
                Phone=x.Phone,
                PasswordHash=x.PasswordHash,
                PasswordSalt=x.PasswordSalt
            }).FirstOrDefaultAsync(x => x.Email == requestEmail);
        }
    }
}
