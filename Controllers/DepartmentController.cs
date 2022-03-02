using EmployeeDirectoryProject.Services.Interfaces;
using EmployeeDirectoryProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectoryProject.Controllers
{
    [Authorize(Roles="Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dept = await _departmentService.GetByIdAsync(id.GetValueOrDefault());
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel dept)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.AddAsync(dept);
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _departmentService.GetByIdAsync(id.GetValueOrDefault());
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentViewModel dept)
        {
            if (id != dept.DepartmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentService.UpdateAsync(dept);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _departmentService.GetByIdAsync(dept.DepartmentID) == null)
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
            return View(dept);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _departmentService.GetByIdAsync(id.GetValueOrDefault());
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
