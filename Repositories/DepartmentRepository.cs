using EmployeeDirectoryProject.DBContext;
using EmployeeDirectoryProject.Models;
using EmployeeDirectoryProject.Repositories.Interfaces;

namespace EmployeeDirectoryProject.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
