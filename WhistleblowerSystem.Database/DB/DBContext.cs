using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;
using WhistleblowerSystem.Shared.Exceptions;

namespace WhistleblowerSystem.Database.DB
{
    public class DbContext : MongoClient, IDbContext
    {
        private static bool _initialized;
        private readonly IMongoDatabase _database;
        private readonly IDictionary<Type, object> _collectionDictonary = new Dictionary<Type, object>();

        public string ConnectionString { get; }
        public string DbName { get; }

        public DbContext(string dbName, string connectionString) : base(connectionString)
        {
            ConnectionString = connectionString;
            DbName = dbName;

            if (_initialized) throw new Exception("MongoDbClinet already initialized. Use Singleton.");
            _database = GetDatabase(dbName);
            //_indexFactory = new IndexFactory(this);
            //todo index creation auslagern
            //CreateIndexAsync().GetAwaiter().GetResult();
            _initialized = true;
        }

        public IEnumerable<Type> GetCollectionTypes()
        {
            return Collections.GetTypes();
        }

        public IEnumerable<string> GetCollectionNames()
        {
            return Collections.GetCollectionNames();
        }

        public async Task DeleteCollectionAsync<T>()
        {
            await _database.DropCollectionAsync(Collections.GetCollectionName<T>());
        }

        public async Task<bool> CheckCollectionExistsAsync<T>()
        {
            var collections = await _database.ListCollectionNamesAsync();
            var collectionNames = await collections.ToListAsync();
            if (collectionNames == null)
            {
                return false;
            }
            else
            {
                return collectionNames.Contains(Collections.GetCollectionName<T>());
            }
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            Type collectionType = typeof(T);
            if (_collectionDictonary.ContainsKey(collectionType)) return _collectionDictonary[collectionType] as IMongoCollection<T> ?? throw new NullException("Collection is not found");
            var collection = _database.GetCollection<T>(Collections.GetCollectionName<T>());
            _collectionDictonary.Add(new KeyValuePair<Type, object>(collectionType, collection));
            return collection;
        }

        public string GetCollectionName<T>()
        {
            return Collections.GetCollectionName<T>();
        }

        public static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer(BsonType.Document));
        }

        //private async Task CreateIndexAsync()
        //{
        //    await _indexFactory.CreateCategoryCollectionIndexes();
        //    await _indexFactory.CreateProductCollectionIndexes();
        //    await _indexFactory.CreateUserCollectionIndexes();
        //    await _indexFactory.CreateBookingCollectionIndexes();
        //    await _indexFactory.CreateCompanyCollectionIndexes();
        //    await _indexFactory.CreateClientCollectionIndexes();
        //    await _indexFactory.CreateLocationCoordinateIndexes();
        //    await _indexFactory.CreateCompanyAdditionCollectionIndexes();
        //    await _indexFactory.CreateCompanyInvoiceNumberIndex();
        //    await _indexFactory.CreateClientPaymentIndex();
        //}

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            var handle = await base.StartSessionAsync();
            return handle;
        }

        public IClientSessionHandle StartSessionSync()
        {
            var handle = StartSession();
            return handle;
        }

        public async Task CreateCollectionsAsync()
        {
            var existingCollections = (await _database.ListCollectionNamesAsync()).ToList();
            foreach (var collectionName in GetCollectionNames().Distinct())
            {
                if (!existingCollections.Contains(collectionName))
                {
                    await _database.CreateCollectionAsync(collectionName);
                }
            }

            //CreateIndexAsync().GetAwaiter().GetResult();
        }
    }
}
