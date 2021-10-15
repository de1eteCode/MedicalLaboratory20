using DataAccess.EFCore.Repositories.EntityRepositories;
using DataModels.Interfaces;
using DataModels.Interfaces.IEntityRepositories;
using System.Threading.Tasks;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LaboratoryContext _laboratoryContext;

        public UnitOfWork(LaboratoryContext context)
        {
            _laboratoryContext = context;

            Insurances = new InsuranceRepository(context);
            Orders = new OrderRepository(context);
            OrderServices = new OrderServiceRepository(context);
            Patients = new PatientRepository(context);
            Safeties = new SafetyRepository(context);
            Services = new ServiceRepository(context);
            Socials = new SocialRepository(context);
            Users = new UserRepository(context);
            UserServices = new UserServiceRepository(context);
        }

        public IInsuranceRepository Insurances { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IOrderServiceRepository OrderServices { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public ISafetyRepository Safeties { get; private set; }
        public IServiceRepository Services { get; private set; }
        public ISocialRepository Socials { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserServiceRepository UserServices { get; private set; }

        public int Complete()
        {
            return _laboratoryContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _laboratoryContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _laboratoryContext.Dispose();
        }
    }
}
