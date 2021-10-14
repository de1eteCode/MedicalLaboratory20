using DataModels.Interfaces.IEntityRepositories;
using System;
using System.Threading.Tasks;

namespace DataModels.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IInsuranceRepository Insurances { get; }
        IOrderRepository Orders { get; }
        IOrderServiceRepository OrderServices { get; }
        IPatientRepository Patients { get; }
        ISafetyRepository Safeties { get; }
        IServiceRepository Services { get; }
        ISocialRepository Socials { get; }
        IUserRepository Users { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
