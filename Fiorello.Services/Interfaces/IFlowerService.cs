using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Flower;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Interfaces
{
    public interface IFlowerService
    {
        public CreatedItemGetDto Create(FlowerCreateDto dto);

        public void Delete(int id);

        public void Update(int id,FlowerUpdateDto dto);

        public FlowerGetDto Get(int id);

        public List<FlowerGetAllItemDto> GetAll();
        
    }
}
