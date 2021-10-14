using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class OrderServiceRepository : GenericRepository<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
