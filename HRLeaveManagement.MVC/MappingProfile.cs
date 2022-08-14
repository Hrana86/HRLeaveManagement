using AutoMapper;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();
    }
}