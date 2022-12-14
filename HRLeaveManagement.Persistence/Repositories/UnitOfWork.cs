using HRLeaveManagement.Application.Constants;
using HRLeaveManagement.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;

namespace HRLeaveManagement.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly LeaveManagmentDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ILeaveAllocationRepository _leaveAllocationRepository;
    private ILeaveRequestRepository _leaveRequestRepository;
    private ILeaveTypeRepository _leaveTypeRepository;

    public UnitOfWork(LeaveManagmentDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public ILeaveAllocationRepository LeaveAllocationRepository => _leaveAllocationRepository ??= new LeaveAllocationRepository(_context);
    public ILeaveRequestRepository LeaveRequestRepository => _leaveRequestRepository ??= new LeaveRequestRepository(_context);
    public ILeaveTypeRepository LeaveTypeRepository => _leaveTypeRepository ??= new LeaveTypeRepository(_context);


    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task Save()
    {
        var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;
        await _context.SaveChangesAsync(username);
    }
}
