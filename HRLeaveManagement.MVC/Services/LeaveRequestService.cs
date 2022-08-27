using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    private readonly IMapper _mapper;

    public LeaveRequestService(IClient httpClient, ILocalStorageService localStorageService, IMapper mapper) : base(localStorageService, httpClient)
    {
        _mapper = mapper;
    }

    public async Task ApproveLeaveRequest(int id, bool approved)
    {
        AddBearerToken();
        try
        {
            var request = new ChangeLeaveRequestApprovalDto { Approved = approved, Id = id };
            await _client.ChangeapprovalAsync(id, request);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest)
    {
        try
        {
            var response = new Response<int>();
            CreateLeaveRequestDto createLeaveRequest = _mapper.Map<CreateLeaveRequestDto>(leaveRequest);
            AddBearerToken();
            var apiResponse = await _client.LeaveRequestsPOSTAsync(createLeaveRequest);
            if (apiResponse.Success)
            {
                response.Data = apiResponse.Id;
                response.Success = true;
            }
            else
            {
                foreach (var error in apiResponse.Errors)
                {
                    response.ValidationErrors += error + Environment.NewLine;
                }
            }
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<int>(ex);
        }
    }

    public async Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        AddBearerToken();
        var leaveRequest = await _client.LeaveRequestsGETAsync(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public Task DeleteLeaveRequest(int leaveId)
    {
        throw new NotImplementedException();
    }

    public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
    {
        AddBearerToken();
        var leaveRequests = await _client.LeaveRequestsAllAsync(false);

        var model = new AdminLeaveRequestViewVM
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(x => x.Approved == true),
            PendingRequests = leaveRequests.Count(x => x.Approved == null),
            RejectedRequests = leaveRequests.Count(x => x.Approved == false),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
    {
        AddBearerToken();
        var leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: true);
        var allocations = await _client.LeaveAllocationsAllAsync(isLoggedInUser: true);
        var model = new EmployeeLeaveRequestViewVM
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }
}