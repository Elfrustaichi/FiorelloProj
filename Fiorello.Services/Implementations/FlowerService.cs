using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Flower;
using Fiorello.Services.Exceptions;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public FlowerService(IFlowerRepository flowerRepository,ICategoryRepository categoryRepository,IFlowerImageService flowerImageService, IHttpContextAccessor accessor, IMapper mapper)
        {
            _flowerRepository = flowerRepository;
            _categoryRepository = categoryRepository;
            _flowerImageService = flowerImageService;
            _accessor = accessor;
            _mapper = mapper;
        }
        public CreatedItemGetDto Create(FlowerCreateDto dto)
        {
            List<RestExceptionError> errors = new List<RestExceptionError>();

            if (_flowerRepository.IsExist(x => x.Name == dto.Name)) errors.Add(new RestExceptionError("Name","Name already exist"));

            if (!_categoryRepository.IsExist(x => x.Id == dto.CategoryId)) errors.Add(new RestExceptionError("CategoryId", "Category not found"));

            if (errors.Count > 0) throw new RestException(System.Net.HttpStatusCode.BadRequest, errors);

            var entity=_mapper.Map<Flower>(dto);

            _flowerRepository.Add(entity);
            _flowerRepository.Commit();

            _flowerImageService.AddPosterImage(entity.Id,dto.PosterImage);
            _flowerImageService.AddImages(entity.Id,dto.Images);
            return new CreatedItemGetDto { Id=entity.Id };
            
        }

        public void Delete(int id)
        {
            if (!_flowerRepository.IsExist(x => x.Id == id)) throw new RestException(System.Net.HttpStatusCode.BadRequest,"Flower not found");

            var entity = _flowerRepository.Get(x=>x.Id==id,"FlowerImages");

            foreach (var img in entity.FlowerImages.ToList())
            {
                _flowerImageService.RemoveImage(img.ImageUrl);
            }

            _flowerRepository.Delete(entity);
            _flowerRepository.Commit();
        }

        public FlowerGetDto Get(int id)
        {
            if(!_flowerRepository.IsExist(x=>x.Id == id)) throw new RestException(System.Net.HttpStatusCode.BadRequest, "Flower not found");

            var entity = _flowerRepository.Get(x=>x.Id==id,"FlowerImages","Category");

            var data = _mapper.Map<FlowerGetDto>(entity);

            var Images=entity.FlowerImages.Where(x=>x.PosterStatus==false).ToList();

            List<string> ImagesList = new List<string>();

            var uriBuilder = new UriBuilder(_accessor.HttpContext.Request.Scheme, _accessor.HttpContext.Request.Host.Host, _accessor.HttpContext.Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;
            foreach (var img in Images)
            {
                var imgUrl = baseUrl + $"/uploads/flowers/{img.ImageUrl}";
                ImagesList.Add(imgUrl);
            }
            data.ImageUrls = ImagesList;
            return data;
        }

        public List<FlowerGetAllItemDto> GetAll()
        {
            var entities = _flowerRepository.GetAll(x=>true,"Category","FlowerImages");
            var data = _mapper.Map<List<FlowerGetAllItemDto>>(entities);

            return data;
        }

        public void Update(int id, FlowerUpdateDto dto)
        {
            var entity=_flowerRepository.Get(x=>x.Id==id,"Category","FlowerImages");

            if (entity == null) throw new RestException(System.Net.HttpStatusCode.BadRequest, "Flower not found");
            List<RestExceptionError> errors = new List<RestExceptionError>();
            if (entity.Name != dto.Name && _flowerRepository.IsExist(x => x.Name == dto.Name)) errors.Add(new RestExceptionError("Name","Name already exist"));

            if (!_categoryRepository.IsExist(x => x.Id == dto.CategoryId)) errors.Add(new RestExceptionError("CategoryId", "Category not found"));

             entity.Name = dto.Name;
            entity.CategoryId = dto.CategoryId;
            entity.Description = dto.Description;
            entity.CostPrice = dto.CostPrice;
            entity.SalePrice = dto.SalePrice;

            string oldPosterImage;

            if(dto.PosterImage!=null)
            {
                oldPosterImage = entity.FlowerImages.FirstOrDefault(x => x.PosterStatus == true).ImageUrl;
                _flowerImageService.AddPosterImage(entity.Id,dto.PosterImage);
                _flowerRepository.Commit();
                _flowerImageService.RemoveImage(oldPosterImage);
            }
            List<string> oldImages = new List<string>();
            if(dto.Images.Any())
            {
                oldImages=entity.FlowerImages.Where(x => x.PosterStatus==false).Select(x=>x.ImageUrl).ToList();
                _flowerImageService.AddImages(entity.Id,dto.Images);
                _flowerRepository.Commit();
                foreach (var img in oldImages)
                {
                    _flowerImageService.RemoveImage(img);
                }

            }
        }
    }
}
