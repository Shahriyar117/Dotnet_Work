using AutoMapper;
using EmployeeDirectoryProject.Models;
using EmployeeDirectoryProject.ViewModels;

namespace EmployeeDirectoryProject.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Employee, EditEmployeeViewModel>().ReverseMap();

        }
    }
}
