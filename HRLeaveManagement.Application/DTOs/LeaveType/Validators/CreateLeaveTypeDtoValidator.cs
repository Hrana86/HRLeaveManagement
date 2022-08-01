using FluentValidation;

namespace HRLeaveManagement.Application.DTOs.LeaveType.Validators;
public class CreateLeaveTypeDtoValidator : AbstractValidator<CreateLeaveTypeDto>
{
    public CreateLeaveTypeDtoValidator()
    {
        Include(new ILeaveTypeDtoValidator());
    }
}