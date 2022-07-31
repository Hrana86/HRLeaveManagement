using HRLeaveManagement.Application.DTOs.LeaveRequestDto;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
public class CreateLeaveTypeCommand : IRequest<int>
{
    public LeaveTypeDto LeaveTypeDto { get; set; }
}