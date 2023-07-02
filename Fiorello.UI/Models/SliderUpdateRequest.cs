namespace Fiorello.UI.Models
{
    public class SliderUpdateRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile BackgroundImage { get; set; }

        public IFormFile SignatureImage { get; set; }
    }
}
