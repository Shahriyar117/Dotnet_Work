using System.ComponentModel.DataAnnotations;

namespace Employee_Directory.Models
{
    public class Department
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentID { get; set; }
        [Required (ErrorMessage ="Please enter the Department Name")]
        public string DepartmentName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
