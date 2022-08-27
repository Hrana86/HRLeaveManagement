using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HRLeaveManagement.Application.Profiles;
using HRLeaveManagement.Application.Responses;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

using Xunit;

namespace HRLeaveManagement.Application.UnitTests.LeaveTypes.Commands;
public class CreateLeaveTypeCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly CreateLeaveTypeDto _leaveTypeDto;
    private readonly CreateLeaveTypeCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _mockUow;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockUow = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(x =>
        {
            x.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _handler = new CreateLeaveTypeCommandHandler(_mockUow.Object, _mapper);

        _leaveTypeDto = new CreateLeaveTypeDto
        {
            DefaultDays = 15,
            Name = "Test DTO"
        };
    }

    [Fact]
    public async Task Valid_LeaveType_Added()
    {
        var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

        var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

        result.ShouldBeOfType<BaseCommandResponse>();

        leaveTypes.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_LeaveType_Added()
    {
        _leaveTypeDto.DefaultDays = -1;

        var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

        var leaveTypes = await _mockUow.Object.LeaveTypeRepository.GetAll();

        leaveTypes.Count.ShouldBe(2);
        result.ShouldBeOfType<BaseCommandResponse>();
    }
}