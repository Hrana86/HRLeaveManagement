using HRLeaveManagement.Application.DTOs.Common;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.DTOs.LeaveAllocation;
public class LeaveAllocationDto : BaseDto
{
    public int NumberOfDays { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public Employee Employee { get; set; }
    public string EmployeeId { get; set; }
}