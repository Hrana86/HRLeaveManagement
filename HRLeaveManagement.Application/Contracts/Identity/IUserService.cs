using HRLeaveManagement.Application.Models.Identity;

namespace HRLeaveManagement.Application.Contracts.Identity;
public interface IUserService
{
    Task<List<Employee>> GetEmployees();
}