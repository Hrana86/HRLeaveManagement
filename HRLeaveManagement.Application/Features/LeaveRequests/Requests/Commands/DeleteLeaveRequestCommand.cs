using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
public class DeleteLeaveRequestCommand : IRequest
{
    public int Id { get; set; }
}