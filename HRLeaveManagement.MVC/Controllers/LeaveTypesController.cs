using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRLeaveManagement.MVC.Controllers;

[Authorize(Roles = "Administrator")]
public class LeaveTypesController : Controller
{
    private readonly ILeaveTypeService _leaveTypeRepository;
    private readonly ILeaveAllocationService _leaveAllocationService;

    public LeaveTypesController(ILeaveTypeService leaveTypeRepository, ILeaveAllocationService leaveAllocationService)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationService = leaveAllocationService;
    }

    // GET: LeaveTypesController
    public async Task<ActionResult> Index()
    {
        var model = await _leaveTypeRepository.GetLeaveTypes();
        return View(model);
    }

    // GET: LeaveTypesController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        var model = await _leaveTypeRepository.GetLeaveTypeDetails(id);
        return View(model);
    }

    // GET: LeaveTypesController/Create
    public async Task<ActionResult> Create()
    {
        return View();
    }

    // POST: LeaveTypesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveTypeVM leaveType)
    {
        try
        {
            var response = await _leaveTypeRepository.CreateLeaveType(leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return View(leaveType);
    }

    // GET: LeaveTypesController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        var model = await _leaveTypeRepository.GetLeaveTypeDetails(id);
        return View(model);
    }

    // POST: LeaveTypesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(LeaveTypeVM leaveType)
    {
        try
        {
            var response = await _leaveTypeRepository.UpdateLeaveType(leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return View(leaveType);
    }

    // POST: LeaveTypesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var response = await _leaveTypeRepository.DeleteLeaveType(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }
        return BadRequest();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Allocate(int id)
    {
        try
        {
            var response = await _leaveAllocationService.CreateLeaveAllocation(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
        }

        return BadRequest();
    }
}
