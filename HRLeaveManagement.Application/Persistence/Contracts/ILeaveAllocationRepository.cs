using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.Persistence.Contracts;
public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
{
    Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
    Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(int id);
}