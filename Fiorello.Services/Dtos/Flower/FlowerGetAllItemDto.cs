using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Dtos.Flower
{
    public class FlowerGetAllItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public string PosterImageUrl { get; set; }

        public List<string> ImageUrls { get; set; }

        public CategoryInFlowerGetAllDto CategoryInFlowerAll { get; set; }
    }

    public class CategoryInFlowerGetAllDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
