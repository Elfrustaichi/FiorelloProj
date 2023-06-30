using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Flower;
using Fiorello.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Implementations
{
    public class FlowerService : IFlowerService
    {
        private readonly IFlowerRepository _flowerRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFlowerImageService _flowerImageService;
        private readonly IMapper _mapper;

        public FlowerService(IFlowerRepository flowerRepository,ICategoryRepository categoryRepository,IFlowerImageService flowerImageService,IMapper mapper)
        {
            _flowerRepository = flowerRepository;
            _categoryRepository = categoryRepository;
            _flowerImageService = flowerImageService;
            _mapper = mapper;
        }
        public CreatedItemGetDto Create(FlowerCreateDto dto)
        {
            if (_flowerRepository.IsExist(x => x.Name == dto.Name)) throw new Exception();

            if(!_categoryRepository.IsExist(x=>x.Id==dto.CategoryId)) throw new Exception();

            var entity=_mapper.Map<Flower>(dto);

            _flowerRepository.Add(entity);
            _flowerRepository.Commit();

            _flowerImageService.AddPosterImage(entity.Id,dto.PosterImage);
            _flowerImageService.AddImages(entity.Id,dto.Images);
            return new CreatedItemGetDto { Id=entity.Id };
            
        }

        public void Delete(int id)
        {
            if(!_flowerRepository.IsExist(x=>x.Id==id)) throw new Exception();

            var entity = _flowerRepository.Get(x=>x.Id==id,"FlowerImages");

            _flowerRepository.Delete(entity);
            _flowerRepository.Commit();

            foreach (var img in entity.FlowerImages)
            {
                _flowerImageService.RemoveImage(img.ImageUrl);
            }
            
        }

        public FlowerGetDto Get(int id)
        {
            
        }

        public List<FlowerGetAllItemDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, FlowerUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
