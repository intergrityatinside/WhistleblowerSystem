using WhistleblowerSystem.Database.Entities;
using WhistleblowerSystem.Database.Interfaces;

namespace WhistleblowerSystem.Database.Repositories
{
    public class FormRepository : BaseRepository<Form>
    {
        public FormRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
