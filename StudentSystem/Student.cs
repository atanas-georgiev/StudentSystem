//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int FacultyNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Specialty { get; set; }
        public int Semester { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public Nullable<int> SpecialtyAndSemesterId { get; set; }
    
        public virtual SpecialtyAndSemester SpecialtyAndSemester { get; set; }
    }
}