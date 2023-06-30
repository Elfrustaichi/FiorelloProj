using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Data.Repositories
{
    public class FlowerImageRepository : IFlowerImageRepository
    {
        private readonly FiorelloDbContext _context;

        public FlowerImageRepository(FiorelloDbContext context)
        {
            _context = context;
        }
        public void Add(FlowerImage entity)
        {
            _context.FlowerImages.Add(entity);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Remove(FlowerImage entity)
        {
            _context.FlowerImages.Remove(entity);
        }
    }
}
