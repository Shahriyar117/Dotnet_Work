using AutoMapper;
using EmployeeDirectoryProject.DBContext;
using EmployeeDirectoryProject.Models;
using EmployeeDirectoryProject.Repositories.Interfaces;
using EmployeeDirectoryProject.Services.Interfaces;
using EmployeeDirectoryProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace EmployeeDirectoryProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper,
                                IWebHostEnvironment hostEnvironment,
                                UserManager<ApplicationUser> userManager)
        {
            _EmployeeRepository = employeeRepository;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        public async Task<List<EmployeeViewModel>> GetAllAsync()
        {
            var employees = await _EmployeeRepository.GetAll();
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<EmployeeViewModel> GetByIdAsync(int id)
        {
            var employees = await _EmployeeRepository.GetById(id);
            if (employees == null)
                return null;
            return _mapper.Map<EmployeeViewModel>(employees);
        }
        public async Task<EditEmployeeViewModel> GetByIdAsyncforEdit(int id)
        {
            var employees = await _EmployeeRepository.GetById(id);
            if (employees == null)
                return null;
            return _mapper.Map<EditEmployeeViewModel>(employees);
        }

        public async Task RemoveAsync(int id)
        {
            var employees = await _EmployeeRepository.GetById(id);
            //var imagePath = Path.Combine(webHostEnvironment.WebRootPath,"_",employees.ProfilePicture );

            //if (System.IO.File.Exists(imagePath))
            //    System.IO.File.Delete(imagePath);
            
            _EmployeeRepository.Remove(employees);
            await _EmployeeRepository.SaveChangingAsync();
                    
            
        }

        public async Task AddAsync(EmployeeViewModel employee)
        {
            
            string NewProfilePicture = UploadedFile(employee);
            var dbEmployee = _mapper.Map<Employee>(employee);
            dbEmployee.ProfilePicture = NewProfilePicture;
            var user = await _userManager.FindByEmailAsync(employee.Email);
            dbEmployee.UserId = user.Id.ToString();
            _EmployeeRepository.Add(dbEmployee);
            await _EmployeeRepository.SaveChangingAsync();       
   
        }
        public async Task UpdateAsync(EditEmployeeViewModel employee)
        {
            
            var dbEmployee = _mapper.Map<Employee>(employee);
           
            _EmployeeRepository.Update(dbEmployee);
            await _EmployeeRepository.SaveChangingAsync();
            
        }
        public async Task<List<EmployeeViewModel>> GetAllByDepartmentAsync(string search)
        {
            var employees = await _EmployeeRepository.GetAllByDepartment(search);
            if (employees == null)
                return null;
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<List<EmployeeViewModel>> GetEmployeeByNameAsync(string search)
        {
            var employees = await _EmployeeRepository.GetEmployeeByName(search);
            if (employees == null)
                return null;
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<List<EmployeeViewModel>> GetEmployeeByEmailAsync(string search)
        {
            var employees = await _EmployeeRepository.GetEmployeeByEmail(search);
            if (employees == null)
                return null;
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<EmployeeViewModel> GetEmployeeByLoginAsync(string search)
        {
            var employees = await _EmployeeRepository.GetEmployeeByLogin(search);
            if (employees == null)
                return null;
            return _mapper.Map<EmployeeViewModel>(employees);
        }
        public async Task<List<EmployeeViewModel>> StringSearchAsync(string search)
        {
            var employees = await _EmployeeRepository.SearchString(search);
            if (employees == null)
                return null;
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<List<EmployeeViewModel>> AscendingEmployeeListAsync(string search)
        {
            var employees = await _EmployeeRepository.AscendingEmployeeList(search);
            if (employees == null)
                return null;
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        public async Task<List<EmployeeViewModel>> DescendingEmployeeListAsync(string search)
        {
            var employees = await _EmployeeRepository.DescendingEmployeeList(search);
            if (employees == null)
                return null;
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }
        private string UploadedFile(EmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
               // string extension = Path.GetExtension(model.ProfileImage.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        //private string UploadedFileEdit(EditEmployeeViewModel model)
        //{
        //    string uniqueFileName = null;

        //    if (model.ProfileImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ProfileImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
    }
}
