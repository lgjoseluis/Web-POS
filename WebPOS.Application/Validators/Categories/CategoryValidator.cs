using FluentValidation;
using WebPOS.Application.Dtos.Request;
using WebPOS.Domain.Entities;

namespace WebPOS.Application.Validators.Categories
{
    public class CategoryValidator : AbstractValidator<CategoryRequestDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo nombre no puede ser vacío");
        }
    }
}

