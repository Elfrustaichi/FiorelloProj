using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Data.Repositories
{
    public class FlowerRepository : Repository<Flower>,IFlowerRepository
    {
        public FlowerRepository(FiorelloDbContext context):base(context)
        {
            
        }
    }
}
