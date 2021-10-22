using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LaboratoryContext context) : base(context)
        {
        }

        public User GetUser(string login, string password)
        {
            return Context.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
        }

        public Task<User> GetUserAsync(string login, string password)
        {
            return Task.FromResult(GetUser(login, password));
        }
    }
}
