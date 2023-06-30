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
    public class SliderRepository : Repository<Slider>,ISliderRepository
    {
        public SliderRepository(FiorelloDbContext context):base(context)
        {
            
        }
    }
}
