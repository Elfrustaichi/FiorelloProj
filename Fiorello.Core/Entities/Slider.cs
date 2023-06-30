using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Core.Entities
{
    public class Slider
    {
        public int Id { get; set; }

        public string SignatureImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string BackgroundImageUrl { get; set; }
    }
}
