using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
