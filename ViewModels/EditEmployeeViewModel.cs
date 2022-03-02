﻿using EmployeeDirectoryProject.DBContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectoryProject.ViewModels
{
    public class EditEmployeeViewModel
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeTitle { get; set; }
        public string UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }

        [ForeignKey("DepartmentID")]
        [Display(Name = "Department Name")]
        public int DepartmentID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }
        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }
        public bool isManoftheMonth { get; set; }
    }
}