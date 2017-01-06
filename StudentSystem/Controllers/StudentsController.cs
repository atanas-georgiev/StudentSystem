using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentSystem;
using StudentSystem.Models.Students;

namespace StudentSystem.Controllers
{
    public class StudentsController : Controller
    {
        private StudentsEntities db = new StudentsEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students;
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Specialties = new SelectList(db.SpecialtyAndSemesters, "Specialty", "Specialty");
            ViewBag.Semesters = new SelectList(db.SpecialtyAndSemesters, "Semester", "Semester");

            var FacultyNumber = 5000;
            if (db.Students.Any())
            {
                FacultyNumber += db.Students.Count();
            }

            return View(new AddStudentViewModel() { FacultyNumber = FacultyNumber });
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student()
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    var a = 1;
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.Specialties = new SelectList(db.SpecialtyAndSemesters, "ID", "Specialty");
            ViewBag.Semesters = new SelectList(db.SpecialtyAndSemesters, "ID", "Semester");
            return View(model);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            //ViewBag.SpecialtyAndSemesterId = new SelectList(db.SpecialtyAndSemesters, "ID", "Specialty", student.SpecialtyAndSemesterId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyNumber,FirstName,LastName,Age,Sex,Specialty,Semester,Email,TelephoneNumber,SpecialtyAndSemesterId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.SpecialtyAndSemesterId = new SelectList(db.SpecialtyAndSemesters, "ID", "Specialty", student.SpecialtyAndSemesterId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
