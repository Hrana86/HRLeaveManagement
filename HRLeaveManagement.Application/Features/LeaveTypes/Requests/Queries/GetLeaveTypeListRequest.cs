using HRLeaveManagement.Application.DTOs.LeaveRequestDto;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
public class GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>
{
}