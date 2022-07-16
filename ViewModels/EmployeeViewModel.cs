using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public int employeeId { get; set; }
        [Required]
        public string employeeName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [ForeignKey("departmentId")]
        public int departmentId { get; set; }
    }
}
