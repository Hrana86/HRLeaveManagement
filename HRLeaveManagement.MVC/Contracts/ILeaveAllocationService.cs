using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts;

public interface ILeaveAllocationService
{
    Task<Response<int>> CreateLeaveAllocation(int leaveTypeId);
}