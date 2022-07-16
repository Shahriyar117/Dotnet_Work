using Project1.DBContext;
using Project1.Models;
using Project1.Repositories.Interfaces;

namespace Project1.Repositories
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
