using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Services.Dtos.Category
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
    }

    public class CategoryUpdateDtoValidator:AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().MaximumLength(20);
        }
    }
}
