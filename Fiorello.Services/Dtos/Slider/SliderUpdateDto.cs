using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Dtos.Slider
{
    public class SliderUpdateDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile BackgroundImage { get; set; }

        public IFormFile SignatureImage { get; set; }
    }

    public class SliderUpdateDtoValidator:AbstractValidator<SliderUpdateDto>
    {
        public SliderUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.BackgroundImage).NotNull();
            RuleFor(x => x.BackgroundImage).NotNull();

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.BackgroundImage != null)
                {
                    if (x.BackgroundImage.Length > 2097152)
                    {
                        context.AddFailure(nameof(x.BackgroundImage), "Background image file must be less than 2MB.");
                    }

                    if (x.BackgroundImage.ContentType != "image/jpeg" && x.BackgroundImage.ContentType != "image/png")
                    {
                        context.AddFailure(nameof(x.BackgroundImage), "Background image file must be image/jpeg and image/png.");
                    }
                }

                if (x.SignatureImage != null)
                {
                    if (x.SignatureImage.Length > 2097152)
                    {
                        context.AddFailure(nameof(x.SignatureImage), "SignatureImage image file must be less than 2MB.");
                    }

                    if (x.SignatureImage.ContentType != "image/jpeg" && x.SignatureImage.ContentType != "image/png")
                    {
                        context.AddFailure(nameof(x.SignatureImage), "SignatureImage image file must be image/jpeg and image/png.");
                    }
                }
            });
        }
    }
}
