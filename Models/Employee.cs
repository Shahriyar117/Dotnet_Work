using EmployeeDirectoryProject.DBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectoryProject.Models
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
        public string Address { get; set; }
        //public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        [Required(ErrorMessage = "Please choose profile image")]
        public string ProfilePicture { get; set; }
        public bool isManoftheMonth { get; set; }
        [ForeignKey("DepartmentID")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        
    }
}
