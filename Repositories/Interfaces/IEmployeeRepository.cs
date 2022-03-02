﻿using EmployeeDirectoryProject.Models;

namespace EmployeeDirectoryProject.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public IList<Department> GetAllDepartments();
        Task<IEnumerable<Employee>> GetAllByDepartment(string search);
        Task<IEnumerable<Employee>> GetEmployeeByName(string search);
        Task<IEnumerable<Employee>> GetEmployeeByEmail(string search);
        Task<Employee> GetEmployeeByLogin(string search);
        Task<IEnumerable<Employee>> SearchString(string search);
        Task<IEnumerable<Employee>> AscendingEmployeeList(string search);
        Task<IEnumerable<Employee>> DescendingEmployeeList(string search);
        IQueryable<Employee> SearchStringQuery(string search);
        IQueryable<Employee> EmployeeAsync();
        IQueryable<Employee> AscendingEmployee();
        IQueryable<Employee> DescendingEmployee();
        IQueryable<Employee> SearchQuery(string search1, string search2, string search3);
        IQueryable<Employee> AscendingDepartment();
        bool ManoftheMonthExists(bool type);
    }
}
