using EmployeeDirectoryProject.DBContext;
using EmployeeDirectoryProject.Models;
using EmployeeDirectoryProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace EmployeeDirectoryProject.Repositories
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
            return _context.Employees.Where(e => e.ApplicationUser.Email == search || search == null).ToList();
        }
        public async Task<Employee> GetEmployeeByLogin(string search)
        {
            return _context.Employees.Where(e => e.ApplicationUser.UserName == search).FirstOrDefault(); 
        }
        public async Task<IEnumerable<Employee>> SearchString(string search)
        {
            return _context.Employees.Where(e => e.Department.DepartmentName.Contains(search)
                                            || e.EmployeeName.Contains(search)
                                            || e.ApplicationUser.Email.Contains(search)
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
                                            || e.ApplicationUser.Email.Contains(search)
                                            || search == null);
        }
        public IQueryable<Employee> SearchQuery(string search1, string search2, string search3)
        {
            var emp = from s in _context.Employees select s;
            if (!String.IsNullOrEmpty(search1))
            {
                emp = emp.Where(e => e.Department.DepartmentName.Contains(search1))
                                 .Include(e => e.ApplicationUser)
                                 .Include(e => e.Department);
                if (!String.IsNullOrEmpty(search2))
                {
                    emp = emp.Where(e => e.ApplicationUser.UserName.Contains(search2))
                                    .Include(e => e.ApplicationUser)
                                    .Include(e => e.Department);
                    if (!String.IsNullOrEmpty(search3))
                    {
                        emp = emp.Where(e => e.ApplicationUser.Email.Contains(search3))
                                         .Include(e => e.ApplicationUser)
                                         .Include(e => e.Department);
                    }
                }
            }
            return emp;
        }
        public IQueryable<Employee> AscendingEmployee()
        {
            return _context.Employees.OrderBy(e => e.EmployeeName)
                                     .Include(e => e.ApplicationUser)
                                     .Include(e => e.Department);
        }
        public IQueryable<Employee> DescendingEmployee()
        {
            return _context.Employees.OrderByDescending(e => e.EmployeeName)
                                     .Include(e => e.ApplicationUser)
                                     .Include(e => e.Department); 
        }
        public IQueryable<Employee> AscendingDepartment()
        {
            return _context.Employees.OrderBy(e => e.Department.DepartmentName)
                                     .Include(e => e.ApplicationUser)
                                     .Include(e => e.Department) ;
        }
        public IQueryable<Employee> EmployeeAsync()
        {
            //var emp = from s in _context.Employees select s;
            var emp = _context.Employees
                .Include(e => e.ApplicationUser)
                .Include(e => e.Department);
            return emp;
        }
        public bool ManoftheMonthExists(bool type)
        {
            if (type == false)
            {
                return true;
            }
            else
            {
                if (_context.Employees.Any(e => e.isManoftheMonth))
                {
                    return false;
                }
                return true;
            }

        }
    }
}
