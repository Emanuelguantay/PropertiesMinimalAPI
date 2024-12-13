using FluentValidation;
using PropertiesMinimalAPI.Models.DTOS;

namespace PropertiesMinimalAPI.Validations
{
    public class ValidationUpdateProperty : AbstractValidator<UpdatePropertyDTO>
    {
        public ValidationUpdateProperty()
        {
            RuleFor(m => m.Id).NotEmpty().GreaterThan(0);
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
            RuleFor(m => m.Location).NotEmpty();
        }
    }
}
