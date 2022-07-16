using Project1.ViewModels;

namespace Project1.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentViewModel>> GetAllAsync();
        Task<DepartmentViewModel> GetByIdAsync(int id);
        Task AddAsync(DepartmentViewModel department);
        Task RemoveAsync(int id);
        Task UpdateAsync(DepartmentViewModel department);
    }
}
