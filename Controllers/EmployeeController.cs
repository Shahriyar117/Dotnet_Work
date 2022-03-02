using Microsoft.AspNetCore.Hosting;
using EmployeeDirectoryProject.Models;
using EmployeeDirectoryProject.Repositories.Interfaces;
using EmployeeDirectoryProject.Services.Interfaces;
using EmployeeDirectoryProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using X.PagedList;
using EmployeeDirectoryProject.DBContext;
using Microsoft.AspNetCore.Authorization;

namespace Employee_Directory.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;    
        public EmployeeController(IEmployeeService employeeService,
                                  IEmployeeRepository employeeRepository,
                                  IWebHostEnvironment hostEnvironment,
                                  UserManager<ApplicationUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string search,
                                               string ?searchlogin,
                                               string? searchemail,
                                               string currentFilter,
                                               string sortOrder,
                                               int? page)
        {
            var view = HttpContext.User.IsInRole("Admin")
                   ? "Index" : "IndexForAnyRole";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DepSortParmAsc = String.IsNullOrEmpty(sortOrder) ? "dept_asc" : "";
            ViewBag.NameSortParmAsc = String.IsNullOrEmpty(sortOrder) ? "name_asc" : "";
            if (search!= null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var emp = _employeeRepository.EmployeeAsync();
            if (!String.IsNullOrEmpty(search))
            {
                //emp = _employeeRepository.SearchStringQuery(search);
                emp = _employeeRepository.SearchQuery(search, searchlogin, searchemail);
                return View(view,emp.ToPagedList(pageNumber,pageSize));
                
            }
            switch (sortOrder)
            {
                case "dept_asc":
                    emp = _employeeRepository.AscendingDepartment();
                    break;

                case "name_asc":
                    emp = _employeeRepository.AscendingEmployee();
                    break;
                default :
                    emp = _employeeRepository.AscendingEmployee();
                    break;
            }
            
            return View(view,emp.ToPagedList(pageNumber, pageSize)); 
        }
        [Authorize(Policy = "writeonlypolicy")]
        public IActionResult Create()
        {
            ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
            ViewBag.RoleName = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            return View();
        }
        [Authorize(Policy = "writeonlypolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = emp.Login,
                    Email = emp.Email,
                    PhoneNumber = emp.WorkPhone,
                };
                ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
                ViewBag.RoleName = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
                if (_employeeRepository.ManoftheMonthExists(emp.isManoftheMonth) == true)
                {
                    var result = await _userManager.CreateAsync(user, emp.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, emp.UserRoles);

                    }
                    await _employeeService.AddAsync(emp);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
                             
                ModelState.AddModelError(string.Empty, "Invalid!!");
            }
            return View(emp);
        }
        [Authorize(Policy = "writeonlypolicy")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
            ViewBag.RoleName = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            var emp = await _employeeService.GetByIdAsyncforEdit(id.GetValueOrDefault());
            if (emp == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(emp.UserId);
            var userrole = await _userManager.GetRolesAsync(user);
            string roles = userrole.FirstOrDefault();
            emp.Email = user.Email;
            emp.Login = user.UserName;
            emp.WorkPhone = user.PhoneNumber;
            emp.UserRoles = roles;
            return View(emp);
        }
        [Authorize(Policy = "writeonlypolicy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditEmployeeViewModel emp)
        {
            if (id != emp.EmployeeID)
            {
                return NotFound();
            }
            ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
            ViewBag.RoleName = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(emp.UserId);
                var userrole = await _userManager.GetRolesAsync(user);
                string roles = userrole.FirstOrDefault();

                user.Email = emp.Email;
                user.UserName = emp.Login;
                user.PhoneNumber = emp.WorkPhone;
                if (_employeeRepository.ManoftheMonthExists(emp.isManoftheMonth) == true)
                {
                    if (roles != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user, roles);
                        await _userManager.AddToRoleAsync(user, emp.UserRoles);
                    }
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await _employeeService.UpdateAsync(emp);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    return View("Man of the Month");
                }
            }
            return View(emp);
        }
        [Authorize(Policy = "writeonlypolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ViewBag.Department = new SelectList(_context.Departments.ToList(), "DepartmentID", "DepartmentName");
            var emp = await _employeeService.GetByIdAsyncforEdit(id.GetValueOrDefault());
            if (emp == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(emp.UserId);
            var userrole = await _userManager.GetRolesAsync(user);
            string roles = userrole.FirstOrDefault();
            emp.Email = user.Email;
            emp.Login = user.UserName;
            emp.WorkPhone = user.PhoneNumber;
            emp.UserRoles = roles;
            return View(emp);
        }
        [Authorize(Policy = "writeonlypolicy")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await _employeeService.GetByIdAsyncforEdit(id);
            var user = await _userManager.FindByIdAsync(emp.UserId);
            var userrole = await _userManager.GetRolesAsync(user);
            string roles = userrole.FirstOrDefault();
            
            if(emp != null)
            {
                await _employeeService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            if (roles != null)
            {
                await _userManager.RemoveFromRoleAsync(user, roles);
                await _userManager.DeleteAsync(user);
                var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "_", emp.ProfilePicture);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }
            return View("Index");                 
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ViewBag.Department = new SelectList(_context.Departments.ToList(), "DepartmentID", "DepartmentName");
            var emp = await _employeeService.GetByIdAsyncforEdit(id.GetValueOrDefault());
            if (emp == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(emp.UserId);
            var userrole = await _userManager.GetRolesAsync(user);
            string roles = userrole.FirstOrDefault();
            emp.Email = user.Email;
            emp.Login = user.UserName;
            emp.WorkPhone = user.PhoneNumber;
            emp.UserRoles = roles;
            return View(emp);
        }
        public List<Department> GetDepartmentDetail()
        {
            List<Department> departmentNames = _employeeRepository.GetAllDepartments().ToList();
            return departmentNames;
        }
        
    }
}
