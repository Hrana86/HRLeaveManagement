using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Domain;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteLeaveTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(DeleteLeaveTypeCommand command, CancellationToken cancellationToken)
    {
        var leaveType = await _unitOfWork.LeaveTypeRepository.Get(command.Id);

        if (leaveType == null)
        {
            throw new NotFoundException(nameof(LeaveType), command.Id);
        }

        await _unitOfWork.LeaveTypeRepository.Delete(leaveType);
        await _unitOfWork.Save();
        return Unit.Value;
    }
}
