using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Application.DTOs.LeaveType;
using HRLeaveManagement.Application.Features.LeaveTypes.Handlers.Queries;
using HRLeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using HRLeaveManagement.Application.Profiles;
using HRLeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HRLeaveManagement.Application.UnitTests.LeaveTypes.Queries;
public class GetLeaveTypeListRequestHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ILeaveTypeRepository> _mockRepo;

    public GetLeaveTypeListRequestHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

        var mapperConfig = new MapperConfiguration(x =>
        {
            x.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

        result.ShouldBeOfType<List<LeaveTypeDto>>();

        result.Count.ShouldBe(2);
    }
}