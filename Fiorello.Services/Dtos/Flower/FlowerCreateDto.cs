using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Dtos.Flower
{
    public class FlowerCreateDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal SalePrice { get; set; }

        public decimal CostPrice { get; set; }

        public int CategoryId { get; set; }

        public IFormFile PosterImage { get; set; }

        public List<IFormFile> Images { get; set; }
    }

    public class FlowerCreateDtoValidator:AbstractValidator<FlowerCreateDto>
    {
        public FlowerCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50); 
            RuleFor(x=>x.Description).NotEmpty().MaximumLength(500);
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(x=>x.CostPrice);
            RuleFor(x=>x.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PosterImage).NotNull();
            RuleFor(x=>x.Images).NotEmpty();

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.PosterImage.Length > 2097152)
                    context.AddFailure(nameof(x.PosterImage), "PosterImage must be less or equal than 2MB");

                if (x.PosterImage.ContentType != "image/jpeg" && x.PosterImage.ContentType != "image/png")
                    context.AddFailure(nameof(x.PosterImage), "PosterImage must be image/jpeg or image/png");

                foreach (var img in x.Images)
                {
                    if (img.Length > 2097152)
                        context.AddFailure(nameof(x.Images), "All Images must be less or equal than 2MB");

                    if (img.ContentType != "image/jpeg" && x.PosterImage.ContentType != "image/png")
                        context.AddFailure(nameof(x.Images), "All Images must be image/jpeg or image/png");
                }
            });
        }
    }
}
