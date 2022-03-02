using EmployeeDirectoryProject.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectoryProject.DBContext
{
    public class ApplicationUser: IdentityUser
    {
        public virtual Employee Employee { get; set; }
    }
}
