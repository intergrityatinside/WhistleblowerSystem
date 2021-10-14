using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;
using WhistleblowerSystem.Shared.Exceptions;

namespace WhistleblowerSystem.Database.Repositories
{
    public abstract class BaseRepository<T> where T : IIdentifiable
    {
        protected IDbContext _dbContext;

        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T?> FindOneAsync(string id)
        {
            return (await _dbContext.GetCollection<T>()
                    .FindAsync(Builders<T>.Filter.Eq(x => x.Id,
                        string.IsNullOrEmpty(id) ? ObjectId.Empty : ObjectId.Parse(id))))
                .FirstOrDefault();

            //return await _dbContext.GetCollection<T>().AsQueryable().Where(x => x.Id == ObjectId.Parse(id))
            //   .FirstOrDefault();
        }

        public async Task InsertOneAsync(T item)
        {
            await _dbContext.GetCollection<T>().InsertOneAsync(item);
        }

        public async Task InsertManyAsync(IEnumerable<T> items)
        {
            await _dbContext.GetCollection<T>().InsertManyAsync(items);
        }

        public async Task DeleteManyAsync(IEnumerable<T> items)
        {
            var ids = items.Select(x => x.Id);
            var filter = Builders<T>.Filter.In(x => x.Id, ids);
            var result = await _dbContext.GetCollection<T>().DeleteManyAsync(filter);
            if (result.DeletedCount != items.Count())
            {
                throw new DbNotModifiedException(result.DeletedCount, DbNotModifiedException.MethodType.Delete);
            }
        }

        public async Task DeleteManyAsync(IEnumerable<string> ids)
        {
            await DeleteManyAsync(ids.Select(x => ObjectId.Parse(x)));
        }

        public async Task DeleteManyAsync(IEnumerable<ObjectId> ids)
        {
            var filter = Builders<T>.Filter.In(x => x.Id, ids);
            var result = await _dbContext.GetCollection<T>().DeleteManyAsync(filter);
            if (result.DeletedCount != ids.Count())
            {
                throw new DbNotModifiedException(result.DeletedCount, DbNotModifiedException.MethodType.Delete);
            }
        }

        public async Task DeleteAsync(T item)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, item.Id);
            var result = await _dbContext.GetCollection<T>().DeleteOneAsync(filter);
            if (result.DeletedCount != 1)
            {
                throw new DbNotModifiedException(result.DeletedCount, DbNotModifiedException.MethodType.Delete);
            }
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.Id, ObjectId.Parse(id));
            var result = await _dbContext.GetCollection<T>().DeleteOneAsync(filter);
            if (result.DeletedCount != 1)
            {
                throw new DbNotModifiedException(result.DeletedCount, DbNotModifiedException.MethodType.Delete);
            }
        }

        public async Task<long> CountAllAsync()
        {
            return await _dbContext.GetCollection<T>().CountDocumentsAsync(x => true);
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await (await _dbContext.GetCollection<T>().FindAsync(x => true)).ToListAsync();
        }

        public async Task<ICollection<T>> GetAllAsync(IEnumerable<string> ids)
        {
            List<ObjectId> objectIds = ids.Select(ObjectId.Parse).ToList();
            return await (await _dbContext.GetCollection<T>().FindAsync(x => objectIds.Contains(x.Id))).ToListAsync();
        }

        public bool IsValidNotEmptyId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId) && !objectId.Equals(ObjectId.Empty);
        }

        public bool IsValidNotEmptyGuid(string guid)
        {
            return Guid.TryParse(guid, out Guid guidParsed) && !guidParsed.Equals(Guid.Empty);
        }

        public bool IsNotEmptyId(ObjectId id)
        {
            return !id.Equals(ObjectId.Empty);
        }

        public bool IsEmptyId(ObjectId id)
        {
            return id.Equals(ObjectId.Empty);
        }

        public ObjectId GetEmptyId()
        {
            return ObjectId.Empty;
        }

        public ObjectId GenerateId()
        {
            return ObjectId.GenerateNewId();
        }
    }
}
