using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Department
    {
        [Key]
        public int departmentId { get; set; }
        [Required]
        public string departmentName { get; set; }
    }
}
