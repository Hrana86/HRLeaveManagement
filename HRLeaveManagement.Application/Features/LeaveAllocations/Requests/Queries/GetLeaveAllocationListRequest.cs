using HRLeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocations.Queries;
public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
{

}