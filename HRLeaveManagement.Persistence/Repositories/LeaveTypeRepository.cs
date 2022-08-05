using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Persistence.Repositories;
public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    private readonly LeaveManagmentDbContext _dbContext;

    public LeaveTypeRepository(LeaveManagmentDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}