using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;
using WhistleblowerSystem.Shared.Enums;

namespace WhistleblowerSystem.Database.Repositories
{
    public class FormRepository : BaseRepository<Form>
    {
        public FormRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddMessage(string id, FormMessage message)
        {
            var update = Builders<Form>.Update.AddToSet(x => x.Messages, message);
            await _dbContext.GetCollection<Form>().UpdateOneAsync(x => x.Id == ObjectId.Parse(id), update);
        }

        public async Task ChangeState(string id, ViolationState state)
        {
            var update = Builders<Form>.Update.Set(x => x.State, state);
            await _dbContext.GetCollection<Form>().UpdateOneAsync(x => x.Id == ObjectId.Parse(id), update);
        }
    }
}
