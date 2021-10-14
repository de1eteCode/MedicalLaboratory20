using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class SafetyRepository : GenericRepository<Safety>, ISafetyRepository
    {
        public SafetyRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
