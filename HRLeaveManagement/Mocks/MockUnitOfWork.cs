using HRLeaveManagement.Application.Contracts.Persistence;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks;
public static class MockUnitOfWork
{
    public static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var mockUow = new Mock<IUnitOfWork>();
        var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

        mockUow.Setup(x => x.LeaveTypeRepository).Returns(mockLeaveTypeRepo.Object);

        return mockUow;
    }
}