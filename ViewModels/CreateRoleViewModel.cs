using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectoryProject.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
