namespace Fiorello.UI.Models
{
    public class FlowerUpdateRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public int CategoryId { get; set; }

        public IFormFile PosterImage { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
