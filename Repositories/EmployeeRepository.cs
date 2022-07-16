using Project1.DBContext;
using Project1.Models;
using Project1.Repositories.Interfaces;

namespace Project1.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public IList<Department> GetAllDepartments()
        {
            var departmentlist = from Department in _context.Departments select Department;
            var departmentnames = departmentlist.ToList<Department>();
            return departmentnames;
        }
    }
}
