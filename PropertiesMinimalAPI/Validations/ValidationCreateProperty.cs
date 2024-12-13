using FluentValidation;
using PropertiesMinimalAPI.Models.DTOS;

namespace PropertiesMinimalAPI.Validations
{
    public class ValidationCreateProperty: AbstractValidator<CreatePropertyDTO>
    {
        public ValidationCreateProperty()
        {

            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
            RuleFor(m => m.Location).NotEmpty();
        }
    }
}
