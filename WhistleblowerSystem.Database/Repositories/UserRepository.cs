using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;

namespace WhistleblowerSystem.Database.Repositories
{
    public class UserRepository
    {
        private readonly IDbContext _dbContext;
        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> CreateUser(string companyId, string passwordHash)
        {
            var user = new User(null, companyId, passwordHash);
            await _dbContext.GetCollection<User>().InsertOneAsync(user);
            return user;
        }

        public async Task<User?> GetByIdAndPassword(string companyId, string id, string passwordHash)
        {
            return await _dbContext.GetCollection<User>().AsQueryable()
                .Where(x => x.Id == ObjectId.Parse(id) &&
                x.PasswordHash == passwordHash
                && x.CompanyId == ObjectId.Parse(companyId))
                .FirstOrDefaultAsync();
        }

        public async Task<User?> FindOneByIdAsync(string id)
        {
            return await _dbContext.GetCollection<User>().AsQueryable()
                    .Where(x => x.Id == ObjectId.Parse(id))
                .FirstOrDefaultAsync();
        }
    }
}
