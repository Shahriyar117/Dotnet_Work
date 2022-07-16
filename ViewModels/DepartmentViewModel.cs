using System.ComponentModel.DataAnnotations;

namespace Project1.ViewModels
{
    public class DepartmentViewModel
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
    }
}
