using coursemanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace coursemanagement.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private ContextDb _context;
        private object CourseViewModel;

        public CourseController()
        {
            _context = new ContextDb();
        }
        // GET: Course
        public ActionResult Index()
        {
            CourseViewModel CourseList = new CourseViewModel
            {
                courses = _context.courses.ToList(),
            };
            return View(CourseList);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PopulateCourseViewModel populateCourseViewModel)
        {
            Course course = new Course
            {
                Id = populateCourseViewModel.Id,
                Name = populateCourseViewModel.Name,
                professeur = populateCourseViewModel.professeur,
                filiere = populateCourseViewModel.filiere,
            };
            _context.courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CourseViewModel courseViewModel, int Id)
        {
            var course = _context.courses.SingleOrDefault(c => c.Id == Id);
            if (course == null)
            {
                ModelState.AddModelError("CNE", "error");
                return View("Index");
            }
            _context.courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Course", CourseViewModel);
        }
        public ActionResult Update(PopulateCourseViewModel courseViewModel, int ? Id )
        {
            var course = _context.courses.SingleOrDefault(c => c.Id == Id);
            courseViewModel.Name = course.Name;
            courseViewModel.professeur = course.professeur;
            courseViewModel.filiere = course.filiere;
            return View(CourseViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PopulateCourseViewModel courseViewModel)
        {
            var Course = _context.courses.Find(courseViewModel.Id);
            if (Course == null)
            {
                ModelState.AddModelError("Id", "something goes wrong");
                return View("Update", courseViewModel);
            }
            Course.Name = courseViewModel.Name;
            Course.professeur = courseViewModel.professeur;
            
            Course.filiere = courseViewModel.filiere;
            Course.Id = courseViewModel.Id;

            _context.courses.Add(Course);
            _context.SaveChanges();

            return View("Index", courseViewModel);
        }





    }
}