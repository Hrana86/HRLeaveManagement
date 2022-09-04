using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeDtoValidator();
        var validationResult = await validator.ValidateAsync(command.LeaveTypeDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }

        var leaveType = await _unitOfWork.LeaveTypeRepository.Get(command.LeaveTypeDto.Id);

        if (leaveType == null)
        {
            throw new NotFoundException(nameof(leaveType), command.LeaveTypeDto.Id);
        }

        _mapper.Map(command.LeaveTypeDto, leaveType);

        await _unitOfWork.LeaveTypeRepository.Update(leaveType);
        await _unitOfWork.Save();

        return Unit.Value;
    }
}