using DataModels.Interfaces.IEntityRepositories;

namespace DataModels.Interfaces.IUnitOfWorks
{
    public interface IUnitRoot : IUnitOfWork
    {
        IInsuranceRepository Insurances { get; }
        IOrderRepository Orders { get; }
        IOrderServiceRepository OrderServices { get; }
        IPatientRepository Patients { get; }
        ISafetyRepository Safeties { get; }
        IServiceRepository Services { get; }
        ISocialRepository Socials { get; }
        IUserRepository Users { get; }
        IUserServiceRepository UserServices { get; }
        IRoleRepository Roles { get; }
    }
}
