using Employee_Directory.DBContext;
using Employee_Directory.Models;
using Employee_Directory.Repositories.Interfaces;

namespace Employee_Directory.Repositories
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
