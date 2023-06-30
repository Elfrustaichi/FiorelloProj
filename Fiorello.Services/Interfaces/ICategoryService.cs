using Fiorello.Services.Dtos.Category;
using Fiorello.Services.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Interfaces
{
    public interface ICategoryService
    {
        public CreatedItemGetDto Create(CategoryCreateDto dto);

        public void Update(int id,CategoryUpdateDto dto);

        public void Delete(int id);

        public List<CategoryGetAllItemDto> GetAll();

        public CategoryGetDto Get(int id);

        
    }
}
