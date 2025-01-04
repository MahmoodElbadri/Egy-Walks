using System.Data;
using FluentValidation;

namespace Egy_Walks.Api.Models.DTOs.WalkDTOs;

public class WalkDtoValidator:AbstractValidator<WalkRequest>
{
    public WalkDtoValidator()
    {
        RuleFor(tmp => tmp.Name)
            .NotEmpty().WithMessage("Walk Name is required")
            .WithName("Walk Name")
            .MaximumLength(50).WithMessage("Walk Name must not exceed 50 characters")
            .MinimumLength(3).WithMessage("Walk Name must be at least 3 characters");
        
        RuleFor(tmp=>tmp.Description)
            .NotEmpty().WithMessage("Walk Description is required")
            .WithName("Walk Description")
            .Length(3,100).WithMessage("Walk Description must be between 100 characters");

        RuleFor(tmp => tmp.DifficultyId)
            .NotEmpty().WithMessage("Difficulty Id is required")
            .WithName("Difficulty Id");
        
        RuleFor(tmp => tmp.RegionId)
            .NotEmpty().WithMessage("Region Id is required")
            .WithName("Region Id");
    }
}