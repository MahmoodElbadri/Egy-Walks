using System.Data;
using FluentValidation;

namespace Egy_Walks.Api.Models.DTOs;

public class RegionDtoValidator:AbstractValidator<RegionRequest>
{
    public RegionDtoValidator()
    {
        RuleFor(tmp=>tmp.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters");
        
        RuleFor(tmp=>tmp.Code)
            .NotEmpty().WithMessage("Code is required")
            .Length(3).WithMessage("Code must not exceed 3 characters");
    }
}