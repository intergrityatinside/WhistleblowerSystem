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
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
