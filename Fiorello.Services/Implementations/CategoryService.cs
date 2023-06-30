using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Data.Repositories;
using Fiorello.Services.Dtos.Category;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public CreatedItemGetDto Create(CategoryCreateDto dto)
        {
            if(_categoryRepository.IsExist(x=>x.Name==dto.Name)) throw new Exception();

            var entity = _mapper.Map<Category>(dto);

            _categoryRepository.Add(entity);
            _categoryRepository.Commit();

            return new CreatedItemGetDto { Id=entity.Id };

        }

        public void Delete(int id)
        {
            if(!_categoryRepository.IsExist(x=>x.Id==id)) throw new Exception();

            var entity=_categoryRepository.Get(x=>x.Id==id);

            _categoryRepository.Delete(entity);
            _categoryRepository.Commit();
        }

        public CategoryGetDto Get(int id)
        {
            if (!_categoryRepository.IsExist(x => x.Id == id)) throw new Exception();

            var entity = _categoryRepository.Get(x => x.Id == id);


            var data = _mapper.Map<CategoryGetDto>(entity);

            return data;
        }

        public List<CategoryGetAllItemDto> GetAll()
        {
            var data = _categoryRepository.GetAll(x=>true);

            return _mapper.Map<List<CategoryGetAllItemDto>>(data);
        }

        public void Update(int id, CategoryUpdateDto dto)
        {
            if(!_categoryRepository.IsExist(x=>x.Id==id)) throw new Exception();

            var entity = _categoryRepository.Get(x=>x.Id==id);

            if(dto.Name!=entity.Name&&_categoryRepository.IsExist(x=>x.Name==dto.Name)) throw new Exception();

            entity.Name = dto.Name;
            _categoryRepository.Commit();


        }
    }
}
