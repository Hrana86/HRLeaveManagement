using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
public class UpdateLeaveAllocationCommand : IRequest<Unit>
{
    public LeaveAllocationDto LeaveAllocationDto { get; set; }
}