using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
public class DeleteLeaveTypeCommand : IRequest
{
    public int Id { get; set; }
}