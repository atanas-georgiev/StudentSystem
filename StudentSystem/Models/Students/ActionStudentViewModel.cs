using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models.Students
{
    public class ActionStudentViewModel
    {
        [Display(Name = "Faculty Number")]
        [Range(5000, 9999)]
        public int FacultyNumber { get; set; }

        public ActionType Action { get; set; }

        public bool AnyStudents { get; set; }
    }
}