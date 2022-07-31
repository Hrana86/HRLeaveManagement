using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
public class DeleteLeaveAllocationCommand : IRequest
{
    public int Id { get; set; }
}