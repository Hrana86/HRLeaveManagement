using HRLeaveManagement.Application.Contracts.Persistence;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks;
public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id=1,
                DefaultDays = 10,
                Name = "Test vacation"
            },
            new LeaveType
            {
                Id=2,
                DefaultDays=15,
                Name = "Test sick"
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(x => x.GetAll()).ReturnsAsync(leaveTypes);

        mockRepo.Setup(x => x.Add(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return leaveType;
        });

        return mockRepo;
    }
}