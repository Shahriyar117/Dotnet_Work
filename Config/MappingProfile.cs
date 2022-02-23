using AutoMapper;
using Employee_Directory.Models;
using Employee_Directory.ViewModels;

namespace Employee_Directory.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

        }
    }
}
