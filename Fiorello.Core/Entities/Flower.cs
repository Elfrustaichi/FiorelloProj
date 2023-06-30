using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Core.Entities
{
    public class Flower
    {
        public int Id { get; set; } 

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<FlowerImage> FlowerImages { get; set; }=new List<FlowerImage>();


    }
}
