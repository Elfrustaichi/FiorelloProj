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

        public string CategoryName { get; set; }

        
    }

    
}
