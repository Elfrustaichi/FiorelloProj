using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Data.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(FiorelloDbContext context) : base(context)
        {
        }
    }
}
