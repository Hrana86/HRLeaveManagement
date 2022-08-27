using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveRequest.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequests.Handlers.Commands;
public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly IUnitOfWork _UnitOfWork;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _UnitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _UnitOfWork.LeaveRequestRepository.Get(request.Id);

        if (request.LeaveRequestDto != null)
        {
            var validator = new UpdateLeaveRequestDtoValidator(_UnitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);
            _mapper.Map(request.LeaveRequestDto, leaveRequest);

            await _UnitOfWork.LeaveRequestRepository.Update(leaveRequest);
            await _UnitOfWork.Save();
        }
        else if (request.ChangeLeaveRequestApprovalDto != null)
        {
            await _UnitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
            if (request.ChangeLeaveRequestApprovalDto.Approved)
            {
                var allocation = await _UnitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                allocation.NumberOfDays -= daysRequested;

                await _UnitOfWork.LeaveAllocationRepository.Update(allocation);
            }

            await _UnitOfWork.Save();
        }

        return Unit.Value;
    }
}