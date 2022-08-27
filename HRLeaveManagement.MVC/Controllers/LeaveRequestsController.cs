using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRLeaveManagement.MVC.Controllers;

[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly ILeaveTypeService _leaveTypeService;
    private readonly ILeaveRequestService _leaveRequestService;


    public LeaveRequestsController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService)
    {
        _leaveTypeService = leaveTypeService;
        _leaveRequestService = leaveRequestService;
    }

    [Authorize(Roles = "Administrator")]
    // GET: LeaveRequestController
    public async Task<ActionResult> Index()
    {
        var model = await _leaveRequestService.GetAdminLeaveRequestList();
        return View(model);
    }

    // GET: LeaveRequestController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var model = await _leaveRequestService.GetLeaveRequest(id);
        return View(model);
    }

    // GET: LeaveRequestController/Create
    public async Task<ActionResult> Create()
    {
        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
        var model = new CreateLeaveRequestVM
        {
            LeaveTypes = leaveTypeItems,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now
        };

        return View(model);
    }

    // POST: LeaveRequestController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
    {
        if (ModelState.IsValid)
        {
            var response = await _leaveRequestService.CreateLeaveRequest(leaveRequest);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }

        var leaveTypes = await _leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
        leaveRequest.LeaveTypes = leaveTypeItems;

        return View(leaveRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> ApproveRequest(int id, bool approved)
    {
        try
        {
            await _leaveRequestService.ApproveLeaveRequest(id, approved);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}