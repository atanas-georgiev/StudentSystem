using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using StudentSystem.Models.Students;

namespace StudentSystem.Controllers
{
    public class StudentsController : Controller
    {
        private const int FirstFacultyNumber = 5000;
        private readonly StudentsEntities db = new StudentsEntities();

        /// <summary>
        ///     Search, Edit, Delete get action
        /// </summary>
        /// <param name="id">Student faculty number</param>
        /// <returns>ActionResult</returns>
        public ActionResult Action(ActionType? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return
                View(new ActionStudentViewModel
                {
                    FacultyNumber = FirstFacultyNumber,
                    Action = id.Value,
                    AnyStudents = db.Students.Any()
                });
        }

        /// <summary>
        ///     Search, Edit, Delete post action. Reditect, based on required action.
        /// </summary>
        /// <param name="model">Input model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Action(ActionStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = db.Students.FirstOrDefault(x => x.FacultyNumber == model.FacultyNumber);

                if (student == null)
                    ModelState.AddModelError("FacultyNumber", "No student found with this faculty number!");
                else
                    switch (model.Action)
                    {
                        case ActionType.Search:
                            return RedirectToAction("Details", new {id = model.FacultyNumber, delete = false});
                        case ActionType.Delete:
                            return RedirectToAction("Details", new {id = model.FacultyNumber, delete = true});
                        case ActionType.Update:
                            return RedirectToAction("Edit", new {id = model.FacultyNumber});
                    }
            }

            return View(model);
        }

        /// <summary>
        /// Details get action
        /// </summary>
        /// <param name="id">Student faculty number</param>
        /// <param name="delete">Bool flag to hide/show delete button</param>
        /// <returns>ActionResult</returns>
        public ActionResult Details(int? id, bool? delete)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = db.Students.FirstOrDefault(x => x.FacultyNumber == id);

            if (student == null)
                return HttpNotFound();

            var studentViewModel = new DetailsStudentViewModel
            {
                FacultyNumber = student.FacultyNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Email = student.Email,
                Semester = student.Semester,
                Sex = student.Sex,
                Specialty = student.Specialty,
                TelephoneNumber = student.TelephoneNumber,
                Disciplines = new List<DisciplineViewModel>(),
                ShowDeleteButton = delete ?? false
            };

            var connections =
                db.ConnectionTables.Where(
                    x =>
                        x.SpecialtyAndSemester.Semester == student.Semester &&
                        x.SpecialtyAndSemester.Specialty == student.Specialty).ToList();

            foreach (var connection in connections)
                studentViewModel.Disciplines.Add(new DisciplineViewModel
                {
                    DisciplineName = connection.Discipline.Name,
                    HoursPerWeek = connection.Discipline.HoursPerWeek,
                    TeacherFirstName = connection.Discipline.Teacher.FirstName,
                    TeacherLastName = connection.Discipline.Teacher.LastName
                });

            return View(studentViewModel);
        }

        /// <summary>
        /// Create student get action
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Create()
        {
            ViewBag.Specialties = new SelectList(db.SpecialtyAndSemesters, "Specialty", "Specialty").DistinctBy(x => x.Value);
            ViewBag.Semesters = new SelectList(db.SpecialtyAndSemesters, "Semester", "Semester").DistinctBy(x => x.Value);

            var number = FirstFacultyNumber;
            if (db.Students.Any())
                number = db.Students.OrderByDescending(x => x.FacultyNumber).First().FacultyNumber + 1;

            return View(new AddStudentViewModel {FacultyNumber = number, Age = 18});
        }

        /// <summary>
        /// Create student post action
        /// </summary>
        /// <param name="model">Input model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FacultyNumber = model.FacultyNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Specialty = model.Specialty,
                    Sex = model.Sex,
                    Semester = model.Semester,
                    Email = model.Email,
                    TelephoneNumber = model.TelephoneNumber
                };

                db.Students.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Specialties = new SelectList(db.SpecialtyAndSemesters, "ID", "Specialty").DistinctBy(x => x.Value);
            ViewBag.Semesters = new SelectList(db.SpecialtyAndSemesters, "ID", "Semester").DistinctBy(x => x.Value);
            return View(model);
        }

        /// <summary>
        /// Edit student get action
        /// </summary>
        /// <param name="id">Student faculty number</param>
        /// <returns>ActionResult</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = db.Students.FirstOrDefault(x => x.FacultyNumber == id);
            if (student == null)
                return HttpNotFound();

            ViewBag.Specialties = new SelectList(db.SpecialtyAndSemesters, "Specialty", "Specialty").DistinctBy(x => x.Value);
            ViewBag.Semesters = new SelectList(db.SpecialtyAndSemesters, "Semester", "Semester").DistinctBy(x => x.Value);

            var studentViewModel = new EditStudentViewModel
            {
                FacultyNumber = student.FacultyNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = student.Age,
                Email = student.Email,
                Semester = student.Semester,
                Sex = student.Sex,
                Specialty = student.Specialty,
                TelephoneNumber = student.TelephoneNumber
            };

            return View(studentViewModel);
        }

        /// <summary>
        /// Edit student post action
        /// </summary>
        /// <param name="model">Input model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = db.Students.FirstOrDefault(x => x.FacultyNumber == model.FacultyNumber);
                if (student == null)
                    return HttpNotFound();

                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Age = model.Age;
                student.Specialty = model.Specialty;
                student.Sex = model.Sex;
                student.Semester = model.Semester;
                student.Email = model.Email;
                student.TelephoneNumber = model.TelephoneNumber;

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");  
            }

            ViewBag.Specialties = new SelectList(db.SpecialtyAndSemesters, "ID", "Specialty").DistinctBy(x => x.Value);
            ViewBag.Semesters = new SelectList(db.SpecialtyAndSemesters, "ID", "Semester").DistinctBy(x => x.Value);
            return View(model);
        }

        /// <summary>
        /// Delete student get action
        /// </summary>
        /// <param name="id">Student faculty number</param>
        /// <returns>ActionResult</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var student = db.Students.Find(id);
            if (student == null)
                return HttpNotFound();

            db.Students.Remove(student);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}