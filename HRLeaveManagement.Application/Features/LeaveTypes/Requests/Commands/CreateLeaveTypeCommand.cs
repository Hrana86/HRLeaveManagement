using HRLeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
public class CreateLeaveTypeCommand : IRequest<int>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; }
}