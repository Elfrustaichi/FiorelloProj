using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Data.Repositories;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Slider;
using Fiorello.Services.Helpers;
using Fiorello.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IMapper _mapper;

        public SliderService(ISliderRepository sliderRepository,IMapper mapper)
        {
            _sliderRepository = sliderRepository;
            _mapper = mapper;
        }
        public CreatedItemGetDto Create(SliderCreateDto dto)
        {
            if (_sliderRepository.IsExist(x => x.Title == dto.Title)) throw new Exception();

            var entity=_mapper.Map<Slider>(dto);

            string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";
            entity.BackgroundImageUrl = FileManager.Save(dto.BackgroundImage,rootPath,"uploads/sliders");
            entity.SignatureImageUrl = FileManager.Save(dto.SignatureImage, rootPath, "uploads/sliders");
            
            _sliderRepository.Add(entity);
            _sliderRepository.Commit();
            
            return new CreatedItemGetDto { Id = entity.Id }; 
           
        }

        public void Delete(int id)
        {
            if(!_sliderRepository.IsExist(x=>x.Id == id)) throw new Exception();

            var entity = _sliderRepository.Get(x=>x.Id==id);

            _sliderRepository.Delete(entity);
            _sliderRepository.Commit();

            string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";
            FileManager.Delete(rootPath,"uploads/sliders",entity.BackgroundImageUrl);
            FileManager.Delete(rootPath,"uploads/sliders",entity.SignatureImageUrl);
        }

        public SliderGetDto Get(int id)
        {
            if(!_sliderRepository.IsExist(x=>x.Id == id)) throw new Exception();

            var entity=_sliderRepository.Get(x=>x.Id==id);

            var data=_mapper.Map<SliderGetDto>(entity);

            return data;
        }

        public List<SliderGetAllItemDto> GetAll()
        {
            var data = _sliderRepository.GetAll(x => true);

            return _mapper.Map<List<SliderGetAllItemDto>>(data);

        }

        public void Update(int id, SliderUpdateDto dto)
        {
            if (!_sliderRepository.IsExist(x => x.Id == id)) throw new Exception();

            var entity = _sliderRepository.Get(x => x.Id == id);

            if(dto.Title!=entity.Title&&_sliderRepository.IsExist(x=>x.Title==dto.Title)) throw new Exception();

            entity.Title = dto.Title;
            entity.Description = dto.Description;


            string removedBackgroundImage = null;
            string removedSignatureImage = null;
            string rootPath = Directory.GetCurrentDirectory() + "/wwwroot";


            if(dto.BackgroundImage!=null)
            {
                removedBackgroundImage = entity.BackgroundImageUrl;
                entity.BackgroundImageUrl = FileManager.Save(dto.BackgroundImage,rootPath,"uploads/sliders");
            }

            if(dto.SignatureImage!=null)
            {
                removedSignatureImage = entity.SignatureImageUrl;
                entity.SignatureImageUrl = FileManager.Save(dto.SignatureImage, rootPath, "uploads/sliders");
            }

            _sliderRepository.Commit();

            if(removedBackgroundImage!=null) FileManager.Delete(rootPath,"uploads/sliders",removedBackgroundImage);

            if(removedSignatureImage!=null) FileManager.Delete(rootPath,"uploads/sliders",removedSignatureImage);
        }
    }
}
