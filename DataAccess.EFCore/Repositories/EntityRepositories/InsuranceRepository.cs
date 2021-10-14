using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class InsuranceRepository : GenericRepository<Insurance>, IInsuranceRepository
    {
        public InsuranceRepository(LaboratoryContext context) : base(context)
        {

        }
    }
}
