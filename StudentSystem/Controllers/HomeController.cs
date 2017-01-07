using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace StudentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentsEntities db = new StudentsEntities();

        public ActionResult Index()
        {
            var studentsCount = db.Students.Count();
            return View(studentsCount);
        }
    }
}