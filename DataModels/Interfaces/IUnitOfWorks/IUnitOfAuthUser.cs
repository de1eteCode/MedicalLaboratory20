using DataModels.Interfaces.IEntityRepositories;
using System;

namespace DataModels.Interfaces.IUnitOfWorks
{
    public interface IUnitOfAuthUser : IUnitOfWork, IDisposable
    {
        IUserRepository Users { get; }
    }
}
