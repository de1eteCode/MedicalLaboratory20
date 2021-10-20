using DataAccess.EFCore.Repositories.EntityRepositories;
using DataModels.Interfaces.IEntityRepositories;
using DataModels.Interfaces.IUnitOfWorks;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfAuthUser : UnitOfWork, IUnitOfAuthUser
    {
        public UnitOfAuthUser(LaboratoryContext context) : base(context)
        {
            Users = new UserRepository(context);
            Auths = new AuthRepository(context);
        }

        public IUserRepository Users { get; }

        public IAuthRepository Auths { get; }
    }
}
