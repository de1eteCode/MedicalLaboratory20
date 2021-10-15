using DataModels.Entities;
using System.Threading.Tasks;

namespace DataModels.Interfaces.IEntityRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUser(string login, string password);
        Task<User> GetUserAsync(string login, string password);
    }
}
