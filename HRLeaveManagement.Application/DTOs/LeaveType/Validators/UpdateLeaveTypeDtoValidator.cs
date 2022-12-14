using FluentValidation;

namespace HRLeaveManagement.Application.DTOs.LeaveType.Validators;
public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
{
    public UpdateLeaveTypeDtoValidator()
    {
        Include(new ILeaveTypeDtoValidator());

        RuleFor(x => x.Id)
            .NotNull().WithMessage("{PropertyName} must be present.");
    }
}