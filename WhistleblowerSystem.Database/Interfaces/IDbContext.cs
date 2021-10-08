using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;

namespace WhistleblowerSystem.Database.Interfaces
{
    public interface IDbContext
    {
        public IMongoCollection<T> GetCollection<T>();
        public Task<bool> CheckCollectionExistsAsync<T>();
        public Task DeleteCollectionAsync<T>();
        public Task<IClientSessionHandle> StartSessionAsync();
        public IClientSessionHandle StartSessionSync();
        public IEnumerable<Type> GetCollectionTypes();
        public IEnumerable<string> GetCollectionNames();
        public string GetCollectionName<T>();
        public Task CreateCollectionsAsync();
    }
}
