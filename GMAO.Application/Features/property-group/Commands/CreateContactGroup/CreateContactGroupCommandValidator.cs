using FluentValidation;

namespace GMAO.Application.Features.property_group.Commands.CreateContactGroup
{
    public class CreateContactGroupCommandValidator : AbstractValidator<CreateContactGroupCommand>
    {
        public CreateContactGroupCommandValidator()
        {
            this.RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters.");

            this.RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters.");

            this.RuleFor(x=> x.Role)
                .IsInEnum().WithMessage("Contact role is invalid.");

            this.RuleFor(x => x.PersonType)
                .IsInEnum().WithMessage("Person type is invalid.");

            this.RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.")
                .MaximumLength(200).WithMessage("Email must not exceed 200 characters.");

            this.RuleFor(x => x.Position)
                .MaximumLength(100).WithMessage("Position must not exceed 100 characters.");

            this.RuleFor(x => x.Department)
                .MaximumLength(100).WithMessage("Department must not exceed 100 characters.");

            this.RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");

            this.RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");

            this.RuleFor(x => x.Mobile)
                .MaximumLength(20).WithMessage("Mobile number must not exceed 20 characters.");

            this.RuleFor(x=> x.PreferredContactMethod)
                .IsInEnum().WithMessage("Preferred contact method is invalid.");

        }
    }
}
