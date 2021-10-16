using DataModels.Interfaces.IEntityRepositories;
using System;

namespace DataModels.Interfaces.IUnitOfWorks
{
    public interface IUnitOfAuthUser : IDisposable
    {
        IUserRepository Users { get; }
    }
}
