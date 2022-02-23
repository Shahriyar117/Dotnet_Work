using Microsoft.AspNetCore.Hosting;
using Employee_Directory.Models;
using Employee_Directory.Repositories.Interfaces;
using Employee_Directory.Services.Interfaces;
using Employee_Directory.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PagedList.Mvc;
using X.PagedList;

namespace Employee_Directory.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;    
        public EmployeeController(IEmployeeService employeeService,
                                  IEmployeeRepository employeeRepository,
                                  UserManager<IdentityUser> userManager,
                                  RoleManager<IdentityRole> roleManager)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _roleManager = roleManager;

    }
        
        public async Task<IActionResult> Index(/*string option,*/string search,
                                               string currentFilter,
                                               string sortOrder,
                                               int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParmDes = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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
                return View( _employeeRepository.SearchStringQuery(search).ToPagedList(pageNumber,pageSize));
                //emp = _employeeRepository.SearchStringQuery(search);
                
            }
            switch (sortOrder)
            {
                case "name_desc":
                    //return View(await _employeeService.DescendingEmployeeListAsync(sortOrder));
                    emp = _employeeRepository.DescendingEmployee();
                    break;

                case "name_asc":
                    //return View(await _employeeService.AscendingEmployeeListAsync(sortOrder));
                    emp = _employeeRepository.AscendingEmployee();
                    break;
                default :
                    //return View(await _employeeService.AscendingEmployeeListAsync(sortOrder));
                    emp = _employeeRepository.AscendingEmployee();
                    break;
            }
            
            return View(emp.ToPagedList(pageNumber, pageSize));
            //return View(_employeeRepository.GetAllPaged(pageNumber, pageSize));
            
            //if (option == "Department")
            //{
            //    return View(await _employeeService.GetAllByDepartmentAsync(search));
            //}
            //else if (option == "EmployeeName")
            //{
            //    return View(await _employeeService.GetEmployeeByNameAsync(search));
            //}
            //else if (option == "Email")
            //{
            //    return View(await _employeeService.GetEmployeeByEmailAsync(search));
            //}
            //else
            //{
            //  return View(await _employeeService.GetAllAsync());
            //} 
        }

        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var emp = await _employeeService.GetByIdAsync(id.GetValueOrDefault());
        //    if (emp == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(emp);
        //}

        public IActionResult Create()
        {
            ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
            ViewBag.Name = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel emp)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = emp.Login,
                    Email = emp.Email,
                };
                var result = await _userManager.CreateAsync(user, emp.Password);
                if (result.Succeeded)
                {
                    //  await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, emp.UserRoles);
                    await _employeeService.AddAsync(emp);
                    return RedirectToAction(nameof(Index));
                }               
                ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
                ViewBag.Name = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid!!");
            }
            return View(emp);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
            ViewBag.Name = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            var emp = await _employeeService.GetByIdAsync(id.GetValueOrDefault());
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel emp)
        {
            if (id != emp.EmployeeID)
            {
                return NotFound();
            }
            //var user = _userManager.Users.FirstOrDefault(u => u.UserName == emp.Login);
            ViewBag.DepartmentName = new SelectList(GetDepartmentDetail(), "DepartmentID", "DepartmentName");
            ViewBag.Name = new SelectList(_roleManager.Roles.Where(u => !u.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(emp.Email);
                    var userrole = await _userManager.GetRolesAsync(user);
                    string roles = userrole.FirstOrDefault();
                    if (user == null)
                    {
                        ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                        return View("NotFound");
                    }
                    else
                    {
                        user.Email = emp.Email;
                        user.UserName = emp.Login;
                        await _userManager.RemoveFromRoleAsync(user, roles);
                        await _userManager.AddToRoleAsync(user, emp.UserRoles);
                        // Update the User using UpdateAsync
                        var result = await _userManager.UpdateAsync(user);
                        await _employeeService.UpdateAsync(emp);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _employeeService.GetByIdAsync(emp.EmployeeID) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //ViewBag.Department = new SelectList(_context.Departments.ToList(), "DepartmentID", "DepartmentName");
            var emp = await _employeeService.GetByIdAsync(id.GetValueOrDefault());
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            var user = await _userManager.FindByEmailAsync(emp.Email);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                await _employeeService.RemoveAsync(id);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }        
        }
        public List<Department> GetDepartmentDetail()
        {
            List<Department> departmentNames = _employeeRepository.GetAllDepartments().ToList();
            return departmentNames;
        }
        
    }
}
