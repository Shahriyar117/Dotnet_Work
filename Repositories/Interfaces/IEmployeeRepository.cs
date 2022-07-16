using Project1.Models;

namespace Project1.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public IList<Department> GetAllDepartments();
    }
}
