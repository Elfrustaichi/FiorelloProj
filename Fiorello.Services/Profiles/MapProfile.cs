using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Services.Dtos.Category;
using Fiorello.Services.Dtos.Flower;
using Fiorello.Services.Dtos.Slider;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile(IHttpContextAccessor accessor)
        {
            //BaseUrl Create start
            var uriBuilder = new UriBuilder(accessor.HttpContext.Request.Scheme, accessor.HttpContext.Request.Host.Host, accessor.HttpContext.Request.Host.Port ?? -1);
            if (uriBuilder.Uri.IsDefaultPort)
            {
                uriBuilder.Port = -1;
            }
            string baseUrl = uriBuilder.Uri.AbsoluteUri;
            //BaseUrl Create end


            //Slider Map start
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<Slider, SliderGetDto>()
                .ForMember(dest => dest.BackgroundImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/sliders/{s.BackgroundImageUrl}"))
                .ForMember(dest => dest.SignatureImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/sliders/{s.SignatureImageUrl}"));
            CreateMap<Slider, SliderGetAllItemDto>()
                .ForMember(dest => dest.SignatureImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/sliders/{s.SignatureImageUrl}"))
                .ForMember(dest => dest.BackgroundImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/sliders/{s.BackgroundImageUrl}"));
            //Slider Map end



            //Category Map start
            CreateMap<CategoryCreateDto,Category>();
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryGetAllItemDto>();
            //Category Map end

            //Flower Map start
            CreateMap<FlowerCreateDto, Flower>();
            CreateMap<Flower, FlowerGetDto>()
                .ForMember(dest => dest.PosterImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/flowers/{s.FlowerImages.FirstOrDefault(x => x.PosterStatus == true).ImageUrl}"));
            CreateMap<Flower, FlowerGetAllItemDto>()
                .ForMember(dest => dest.PosterImageUrl, m => m.MapFrom(s => baseUrl + $"uploads/flowers/{s.FlowerImages.FirstOrDefault(x => x.PosterStatus == true).ImageUrl}"));
            CreateMap<Category, CategoryInFlowerGetDto>();
            //Flower Map end

        }
    }
}
