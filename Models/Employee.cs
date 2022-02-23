using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee_Directory.Models
{
    public class Employee
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "Please enter the Employee Name")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage ="Please enter the Employee Title")]
        public string EmployeeTitle { get; set; }
        [Required(ErrorMessage = "Please enter the Employee Email")]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        [Required(ErrorMessage = "Please choose profile image")]
        public string ProfilePicture { get; set; }
        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }
    }
}
