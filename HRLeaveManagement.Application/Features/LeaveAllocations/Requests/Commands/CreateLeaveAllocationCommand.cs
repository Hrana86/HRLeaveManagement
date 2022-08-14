using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using HRLeaveManagement.Application.Responses;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
}