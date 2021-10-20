using DataModels.Interfaces.IEntityRepositories;
using System;

namespace DataModels.Interfaces.IUnitOfWorks
{
    public interface IUnitOfAuthUser : IUnitOfWork, IDisposable
    {
        IAuthRepository Auths { get; }
        IUserRepository Users { get; }
    }
}
