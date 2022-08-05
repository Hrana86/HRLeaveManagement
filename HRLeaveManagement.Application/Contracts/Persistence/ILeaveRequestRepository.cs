using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Contracts.Persistence;
public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
    Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus);
}