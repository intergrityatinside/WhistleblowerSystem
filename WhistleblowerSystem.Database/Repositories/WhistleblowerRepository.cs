using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;

namespace WhistleblowerSystem.Database.Repositories
{
    public class WhistleblowerRepository : BaseRepository<Whistleblower>
    {
        public WhistleblowerRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Whistleblower?> GetByFormId(string formId)
        {
            return await _dbContext.GetCollection<Whistleblower>().AsQueryable()
                .Where(x => x.FormId == ObjectId.Parse(formId))
                .FirstOrDefaultAsync();
        }
    }
}
