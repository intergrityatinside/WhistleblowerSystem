using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;

namespace WhistleblowerSystem.Database.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.GetCollection<User>().AsQueryable()
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }

    }
}
