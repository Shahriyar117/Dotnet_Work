using Employee_Directory.ViewModels;

namespace Employee_Directory.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetAllAsync();
        Task<EmployeeViewModel> GetByIdAsync(int id);
        Task AddAsync(EmployeeViewModel employee);
        Task RemoveAsync(int id);
        Task UpdateAsync(EmployeeViewModel employee);
        Task<List<EmployeeViewModel>> GetAllByDepartmentAsync(string search);
        Task<List<EmployeeViewModel>> GetEmployeeByNameAsync(string search);
        Task<List<EmployeeViewModel>> GetEmployeeByEmailAsync(string search);
        Task<EmployeeViewModel> GetEmployeeByLoginAsync(string search);
        Task<List<EmployeeViewModel>> StringSearchAsync(string search);
        Task<List<EmployeeViewModel>> AscendingEmployeeListAsync(string search);
        Task<List<EmployeeViewModel>> DescendingEmployeeListAsync(string search);
    }
}
