using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models.Students
{
    public abstract class StudentViewModelBase
    {
        [Display(Name = "Faculty Number")]
        public int FacultyNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Age")]
        [Range(18, 99)]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string Specialty { get; set; }

        [Required]
        [Display(Name = "Semester")]
        public int Semester { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        [RegularExpression("^(87[6-9]|88[2-9]|89[0-9]|988)[0-9]{6}$", ErrorMessage = "Invalid Telephone number!")]
        public string TelephoneNumber { get; set; }
    }
}