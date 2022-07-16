using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.DBContext;
using Project1.Models;
using Project1.Services.Interfaces;

namespace Project1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context, IDepartmentService departmentService)
        {
            _context = context;
            _departmentService = departmentService;
        }
        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            return await _context.Departments.ToListAsync();
        }
        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            var dep = await _context.Departments.FindAsync(id);
            if (dep == null)
            {
                return NotFound();
            }
            return dep;
        }
        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department dep)
        {
            if (id != dep.departmentId)
            {
                return BadRequest();
            }
            _context.Entry(dep).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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
        public async Task<ActionResult<Department>> PostDepartment(Department dep)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'DBContext.Departments'  is null.");
            }
            _context.Departments.Add(dep);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartment), new { id = dep.departmentId }, dep);
        }
        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (_context.Departments == null)
            {
                return NotFound();
            }
            var dep = await _context.Departments.FindAsync(id);
            if (dep == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(dep);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool DepartmentExists(int id)
        {
            return (_context.Departments?.Any(e => e.departmentId == id)).GetValueOrDefault();
        }
    }
}
