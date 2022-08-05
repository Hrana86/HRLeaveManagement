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

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(int id)
    {
        var leaveAllocations = await _dbContext.LeaveAllocations
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
}