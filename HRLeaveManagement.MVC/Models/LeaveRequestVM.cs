using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.MVC.Models;

public class LeaveRequestVM : CreateLeaveRequestVM
{
    public int Id { get; set; }

    [Display(Name = "Date requested")]
    public DateTime DateRequested { get; set; }

    [Display(Name = "Date actioned")]
    public DateTime DateActioned { get; set; }

    [Display(Name = "Approval state")]
    public bool? Approved { get; set; }

    public bool Cancelled { get; set; }
    public LeaveTypeVM LeaveType { get; set; }
    public EmployeeVM Employee { get; set; }
}

public class CreateLeaveRequestVM
{
    [Display(Name = "Start date"), Required]
    public DateTime StartDate { get; set; }

    [Display(Name = "End date"), Required]
    public DateTime EndDate { get; set; }

    public SelectList? LeaveTypes { get; set; }

    [Display(Name = "Leave type")]
    public int LeaveTypeId { get; set; }

    [Display(Name = "Comments"), MaxLength(300)]
    public string RequestComments { get; set; }
}