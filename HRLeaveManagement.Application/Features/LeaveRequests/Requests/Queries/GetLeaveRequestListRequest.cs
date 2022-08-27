using HRLeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
{
    public bool IsLoggedInUser { get; set; }
}