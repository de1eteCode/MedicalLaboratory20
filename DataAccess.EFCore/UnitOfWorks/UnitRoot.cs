using DataAccess.EFCore.Repositories.EntityRepositories;
using DataModels.Interfaces.IEntityRepositories;
using DataModels.Interfaces.IUnitOfWorks;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitRoot : UnitOfWork, IUnitRoot
    {
        public UnitRoot(LaboratoryContext context) : base(context)
        {
            Insurances = new InsuranceRepository(context);
            Orders = new OrderRepository(context);
            OrderServices = new OrderServiceRepository(context);
            Patients = new PatientRepository(context);
            Safeties = new SafetyRepository(context);
            Services = new ServiceRepository(context);
            Socials = new SocialRepository(context);
            Users = new UserRepository(context);
            UserServices = new UserServiceRepository(context);
            Roles = new RoleRepository(context);
            Auths = new AuthRepository(context);
        }

        public IInsuranceRepository Insurances { get; }

        public IOrderRepository Orders { get; }

        public IOrderServiceRepository OrderServices { get; }

        public IPatientRepository Patients { get; }

        public ISafetyRepository Safeties { get; }

        public IServiceRepository Services { get; }

        public ISocialRepository Socials { get; }

        public IUserRepository Users { get; }

        public IUserServiceRepository UserServices { get; }

        public IRoleRepository Roles { get; }
        public IAuthRepository Auths { get; }
    }
}
