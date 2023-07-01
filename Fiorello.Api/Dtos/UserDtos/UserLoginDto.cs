using FluentValidation;

namespace Fiorello.Api.Dtos.UserDtos
{
    public class UserLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class UserLoginDtoValidator:AbstractValidator<UserLoginDto> 
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(20);
            RuleFor(x=>x.Password).NotEmpty().MinimumLength(8);
        }
    }

}
