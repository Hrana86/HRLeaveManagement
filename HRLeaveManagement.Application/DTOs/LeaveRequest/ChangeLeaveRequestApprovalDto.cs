using HRLeaveManagement.Application.DTOs.Common;

namespace HRLeaveManagement.Application.DTOs.LeaveRequest;
public class ChangeLeaveRequestApprovalDto : BaseDto
{
    public bool Approved { get; set; }
}