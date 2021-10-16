using System;
using System.Threading.Tasks;

namespace DataModels.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();
        Task<int> CompleteAsync();
    }
}
