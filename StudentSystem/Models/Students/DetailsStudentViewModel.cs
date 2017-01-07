using System.Collections.Generic;

namespace StudentSystem.Models.Students
{
    public class DetailsStudentViewModel : StudentViewModelBase
    {
        public IList<DisciplineViewModel> Disciplines { get; set; }

        public bool ShowDeleteButton { get; set; }
    }
}