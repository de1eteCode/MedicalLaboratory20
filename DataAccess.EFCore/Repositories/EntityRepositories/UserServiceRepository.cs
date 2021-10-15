using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class UserServiceRepository : GenericRepository<UserService>, IUserServiceRepository
    {
        public UserServiceRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
