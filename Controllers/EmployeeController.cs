using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.DBContext;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.ToListAsync();
            return employees;
        }
        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return emp;
        }
        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee emp)
        {
            if (id != emp.employeeId)
            {
                return BadRequest();
            }
            _context.Entry(emp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Department>> PostEmployee(Employee emp)
        {
            if (_context.Employees == null)
            {
                return Problem("Entity set 'DBContext.Departments'  is null.");
            }
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.employeeId }, emp);
        }
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.employeeId == id)).GetValueOrDefault();
        }
        private IList<Department> GetAllDepartments()
        {
            var departmentlist = from Department in _context.Departments select Department;
            var departmentnames = departmentlist.ToList<Department>();
            return departmentnames;
        }
    }
}
