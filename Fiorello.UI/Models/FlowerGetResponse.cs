namespace Fiorello.UI.Models
{
    public class FlowerGetResponse
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public string PosterImageUrl { get; set; }

        public List<string> ImageUrls { get; set; }

        public CategoryInFlowerGetResponse Category { get; set; }
    }
    public class CategoryInFlowerGetResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
