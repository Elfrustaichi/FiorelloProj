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
    public class CategoryRepository :Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(FiorelloDbContext context):base(context)
        {
            
        }
       
    }
}
