using Fiorello.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Core.Repositories
{
    public interface IFlowerImageRepository
    {
        public void Add(FlowerImage entity);

        public void Remove(FlowerImage entity);

        public void Commit();

    }
}
