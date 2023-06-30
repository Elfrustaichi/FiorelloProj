using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Core.Entities
{
    public class FlowerImage
    {
        public int Id { get; set; }

        public int FlowerId { get; set; }

        public bool PosterStatus { get; set; }

        public string ImageUrl { get; set; }

        public Flower Flower { get; set; }
    }
}
