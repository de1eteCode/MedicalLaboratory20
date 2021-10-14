using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
