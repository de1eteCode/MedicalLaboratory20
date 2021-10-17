using DataModels.Interfaces;
using System.Threading.Tasks;

namespace DataAccess.EFCore
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly LaboratoryContext _laboratoryContext;

        public UnitOfWork(LaboratoryContext context)
        {
            _laboratoryContext = context;
        }

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
