using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;

namespace WhistleblowerSystem.Database.Repositories
{
    public class FormTemplateRepository : BaseRepository<FormTemplate>
    {
        public FormTemplateRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
