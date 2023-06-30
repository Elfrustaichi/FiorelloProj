using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Dtos.Slider
{
    public class SliderGetDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string BackgroundImageUrl { get; set; }

        public string SignatureImageUrl { get; set; }
    }
}
