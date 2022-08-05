using HRLeaveManagement.Application.DTOs.LeaveRequest;
using HRLeaveManagement.Application.Responses;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
public class CreateLeaveRequestCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveRequestDto LeaveRequestDto { get; set; }
}