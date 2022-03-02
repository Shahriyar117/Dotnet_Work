using AutoMapper;
using EmployeeDirectoryProject.Models;
using EmployeeDirectoryProject.Repositories.Interfaces;
using EmployeeDirectoryProject.Services.Interfaces;
using EmployeeDirectoryProject.Services.Interfaces;
using EmployeeDirectoryProject.ViewModels;

namespace EmployeeDirectoryProject.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _DepartmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository DepartmentRepository, IMapper mapper)
        {
            _DepartmentRepository = DepartmentRepository;
            _mapper = mapper;
        }

        public async Task<List<DepartmentViewModel>> GetAllAsync()
        {
            var departments = await _DepartmentRepository.GetAll();
            return _mapper.Map<List<DepartmentViewModel>>(departments);
        }

        public async Task<DepartmentViewModel> GetByIdAsync(int id)
        {
            var departments = await _DepartmentRepository.GetById(id);
            if (departments == null)
                return null;
            return _mapper.Map<DepartmentViewModel>(departments);
        }

        public async Task RemoveAsync(int id)
        {
            var departments = await _DepartmentRepository.GetById(id);
            _DepartmentRepository.Remove(departments);
            await _DepartmentRepository.SaveChangingAsync();
        }

        public async Task AddAsync(DepartmentViewModel department)
        {
            var dbDepartment = _mapper.Map<Department>(department);
            _DepartmentRepository.Add(dbDepartment);
            await _DepartmentRepository.SaveChangingAsync();
        }
        public async Task UpdateAsync(DepartmentViewModel department)
        {
            var dbDepartment = _mapper.Map<Department>(department);
            _DepartmentRepository.Update(dbDepartment);
            await _DepartmentRepository.SaveChangingAsync();
        }
    }
}
