using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Directory.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeTitle { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfileImage { get; set; }

        [ForeignKey("DepartmentID")]
        [Display(Name = "Department Name")]
        public int DepartmentID { get; set; }
        [Required]
        public string Login  { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm Password")]
        //[Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        //public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }
    }
}
