using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts;

public interface ILeaveTypeService
{
    Task<List<LeaveTypeVM>> GetLeaveTypes();
    Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType);
    Task<Response<int>> UpdateLeaveType(LeaveTypeVM leaveType);
    Task<Response<int>> DeleteLeaveType(int id);
}