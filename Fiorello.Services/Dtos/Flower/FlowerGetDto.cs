using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Dtos.Flower
{
    public class FlowerGetDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public string PosterImageUrl { get; set; }

        public List<string> ImageUrls { get; set;}

        public CategoryInFlowerGetDto Category { get; set; }
    }

    public class CategoryInFlowerGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
