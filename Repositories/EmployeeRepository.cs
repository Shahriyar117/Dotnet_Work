using Employee_Directory.DBContext;
using Employee_Directory.Models;
using Employee_Directory.Repositories.Interfaces;

namespace Employee_Directory.Repositories
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
        public async Task<IEnumerable<Employee>> GetAllByDepartment(string search)
        {
            return _context.Employees.Where(e => e.Department.DepartmentName == search || search == null).ToList();
        }
        public async Task<IEnumerable<Employee>> GetEmployeeByName(string search)
        {
            return _context.Employees.Where(e => e.EmployeeName == search || search == null).ToList();
        }
        public async Task<IEnumerable<Employee>> GetEmployeeByEmail(string search)
        {
            return _context.Employees.Where(e => e.Email == search || search == null).ToList();
        }
        public async Task<Employee> GetEmployeeByLogin(string search)
        {
            return _context.Employees.Where(e => e.Login == search).FirstOrDefault(); 
        }
        public async Task<IEnumerable<Employee>> SearchString(string search)
        {
            return _context.Employees.Where(e => e.Department.DepartmentName.Contains(search)
                                            || e.EmployeeName.Contains(search)
                                            || e.Email.Contains(search)
                                            ||search == null).ToList();
        }
        public async Task<IEnumerable<Employee>> AscendingEmployeeList (string search)
        {
            return _context.Employees.OrderBy(e => e.EmployeeName).ToList();
        }
        public async Task<IEnumerable<Employee>> DescendingEmployeeList(string search)
        {
            return _context.Employees.OrderByDescending(e => e.EmployeeName).ToList();
        }

        public IQueryable<Employee> SearchStringQuery(string search)
        {
            return _context.Employees.Where(e => e.Department.DepartmentName.Contains(search)
                                            || e.EmployeeName.Contains(search)
                                            || e.Email.Contains(search)
                                            || search == null);
        }
        public IQueryable<Employee> AscendingEmployee()
        {
            return _context.Employees.OrderBy(e => e.EmployeeName);
        }
        public IQueryable<Employee> DescendingEmployee()
        {
            return _context.Employees.OrderByDescending(e => e.EmployeeName);
        }

        public IQueryable<Employee> EmployeeAsync()
        {
            var emp = from s in _context.Employees select s;
            return emp;
        }

    }
}
