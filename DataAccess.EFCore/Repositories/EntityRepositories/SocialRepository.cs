using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class SocialRepository : GenericRepository<Social>, ISocialRepository
    {
        public SocialRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
