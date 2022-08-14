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
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly CreateLeaveTypeDto _leaveTypeDto;
    private readonly CreateLeaveTypeCommandHandler _handler;

    public CreateLeaveTypeCommandHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(x =>
        {
            x.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);

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

        var leaveTypes = await _mockRepo.Object.GetAll();

        result.ShouldBeOfType<BaseCommandResponse>();

        leaveTypes.Count.ShouldBe(3);
    }

    [Fact]
    public async Task InValid_LeaveType_Added()
    {
        _leaveTypeDto.DefaultDays = -1;

        var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);

        var leaveTypes = await _mockRepo.Object.GetAll();

        leaveTypes.Count.ShouldBe(2);
        result.ShouldBeOfType<BaseCommandResponse>();
    }
}