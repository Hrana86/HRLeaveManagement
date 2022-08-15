using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services;

public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
{
    public LeaveAllocationService(IClient httpClient, ILocalStorageService localStorageService) : base(localStorageService, httpClient)
    {
    }

    public async Task<Response<int>> CreateLeaveAllocation(int leaveTypeId)
    {
        try
        {
            var response = new Response<int>();
            CreateLeaveAllocationDto createLeaveAllocation = new CreateLeaveAllocationDto() { LeaveTypeId = leaveTypeId };
            AddBearerToken();
            var apiResponse = await _client.LeaveAllocationsPOSTAsync(createLeaveAllocation);
            if (apiResponse.Success)
            {
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
}