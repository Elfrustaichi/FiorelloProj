using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Interfaces
{
    public interface IFlowerImageService
    {
        public void AddPosterImage(int flowerId,IFormFile Image);

        public void AddImages(int flowerId,List<IFormFile> Images);

        public void RemoveImage(string ImageName);

        public 
    }
}
