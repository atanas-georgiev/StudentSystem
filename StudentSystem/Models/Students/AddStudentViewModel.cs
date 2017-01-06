using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentSystem.Models.Students
{
    public class AddStudentViewModel
    {        
        public int FacultyNumber { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Specialty { get; set; }
        public int Semester { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
    }
}