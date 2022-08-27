using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.MVC.Models;

public class AdminLeaveRequestViewVM
{
    [Display(Name = "Total number of requests")]
    public int TotalRequests { get; set; }

    [Display(Name = "Approved request")]
    public int ApprovedRequests { get; set; }

    [Display(Name = "Pending request")]
    public int PendingRequests { get; set; }

    [Display(Name = "Rejected requests")]
    public int RejectedRequests { get; set; }
    public List<LeaveRequestVM> LeaveRequests { get; set; }
}