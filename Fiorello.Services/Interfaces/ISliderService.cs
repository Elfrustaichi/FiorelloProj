using Fiorello.Services.Dtos.Category;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Slider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Interfaces
{
    public interface ISliderService
    {
        public CreatedItemGetDto Create(SliderCreateDto dto);

        public void Update(int id, SliderUpdateDto dto);

        public void Delete(int id);

        public List<SliderGetAllItemDto> GetAll();

        public SliderGetDto Get(int id);
    }
}
