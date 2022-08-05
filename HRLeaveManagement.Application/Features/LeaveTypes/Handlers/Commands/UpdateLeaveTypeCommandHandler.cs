using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType.Validators;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
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

        var leaveType = await _leaveTypeRepository.Get(command.LeaveTypeDto.Id);

        _mapper.Map(command.LeaveTypeDto, leaveType);

        await _leaveTypeRepository.Update(leaveType);

        return Unit.Value;
    }
}