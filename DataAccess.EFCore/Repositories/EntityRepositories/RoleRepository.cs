using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
