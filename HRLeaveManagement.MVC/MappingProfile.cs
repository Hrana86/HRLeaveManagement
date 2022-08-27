using AutoMapper;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVM>().ReverseMap();
        CreateMap<LeaveRequestDto, LeaveRequestVM>()
            .ForMember(x => x.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveRequestListDto, LeaveRequestVM>()
            .ForMember(x => x.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
        CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();
        CreateMap<EmployeeVM, Employee>().ReverseMap();
    }
}