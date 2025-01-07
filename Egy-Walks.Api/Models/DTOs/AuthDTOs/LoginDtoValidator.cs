using FluentValidation;

namespace Egy_Walks.Api.Models.DTOs.AuthDTOs;

public class LoginDtoValidator:AbstractValidator<LoginAddRequest>
{
    public LoginDtoValidator()
    {
        RuleFor(tmp => tmp.UserName)
            .NotEmpty()
            .WithMessage("Username cannot be empty")
            .MaximumLength(100)
            .WithMessage("Username cannot exceed 100 characters")
            .WithName("User name")
            .EmailAddress()
            .WithMessage("Invalid Email Address")
            .MinimumLength(4)
            .WithMessage("Enter valid Email Address");
        
        RuleFor(tmp => tmp.Password)
            .NotEmpty()
            .WithMessage("Password cannot be empty")
            .MaximumLength(100)
            .WithMessage("Password cannot exceed 100 characters")
            .WithName("Password")
            .MinimumLength(6)
            .WithMessage("Enter valid Password");
    }
}