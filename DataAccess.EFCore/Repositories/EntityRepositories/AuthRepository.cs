using DataModels.Entities;
using DataModels.Interfaces.IEntityRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Repositories.EntityRepositories
{
    public class AuthRepository : GenericRepository<Auth>, IAuthRepository
    {
        public AuthRepository(LaboratoryContext context) : base(context)
        {
        }
    }
}
