using HRLeaveManagement.Application.DTOs.LeaveRequestDto;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
public class GetLeaveTypeDetailsRequest : IRequest<LeaveTypeDto>
{
    public int Id { get; set; }
}