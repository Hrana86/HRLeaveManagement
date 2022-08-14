using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Responses;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; }
}