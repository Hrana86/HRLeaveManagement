using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistence.Repositories;
public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagmentDbContext _dbContext;

    public LeaveAllocationRepository(LeaveManagmentDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _dbContext.AddRangeAsync(allocations);
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _dbContext.LeaveAllocations.AnyAsync(x => x.EmployeeId == userId
        && x.LeaveTypeId == leaveTypeId && x.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
            .Include(x => x.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
            .Where(x => x.EmployeeId == userId)
            .Include(x => x.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await _dbContext.LeaveAllocations
           .Include(x => x.LeaveType)
           .FirstOrDefaultAsync(x => x.Id == id);

        return leaveAllocation;
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _dbContext.LeaveAllocations.FirstOrDefaultAsync(x => x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
    }
}