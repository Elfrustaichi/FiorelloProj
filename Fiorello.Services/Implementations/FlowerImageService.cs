using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Services.Helpers;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Implementations
{
    public class FlowerImageService : IFlowerImageService
    {
        private readonly IFlowerImageRepository _flowerImageRepository;
        private readonly string _rootPath;

        public FlowerImageService(IFlowerImageRepository flowerImageRepository)
        {
            _flowerImageRepository = flowerImageRepository;
            _rootPath = Directory.GetCurrentDirectory() + "/wwwroot";
        }
        public void AddPosterImage(int flowerId, IFormFile Image)
        {
            string newFileName=FileManager.Save(Image, _rootPath,"uploads/flowers");

            FlowerImage flowerImage = new FlowerImage
            {
                ImageUrl = newFileName,
                FlowerId = flowerId,
                PosterStatus=true
            };

            _flowerImageRepository.Add(flowerImage);
            _flowerImageRepository.Commit();

        }

        public void AddImages(int flowerId, List<IFormFile> Images)
        {
            
            foreach (IFormFile Image in Images)
            {
                string newFileName = FileManager.Save(Image, _rootPath, "uploads/flowers");
                FlowerImage flowerImage = new FlowerImage
                {
                    ImageUrl= newFileName,
                    FlowerId=flowerId,
                    PosterStatus = false
                };

                _flowerImageRepository.Add(flowerImage);
            }

            _flowerImageRepository.Commit();
        }

        

        public void RemoveImage(string ImageName)
        {
            FileManager.Delete(_rootPath,"uploads/flowers", ImageName);
        }
    }
}
