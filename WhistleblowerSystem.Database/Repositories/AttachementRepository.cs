using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;

namespace WhistleblowerSystem.Database.Repositories
{
    public class AttachementRepository : BaseRepository<Attachement>
    {
        public AttachementRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
