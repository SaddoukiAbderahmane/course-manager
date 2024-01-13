using coursemanagement.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace coursemanagement.Controllers
{
    public class HomeController : Controller
    {
        private ContextDb _context;
        public HomeController()
        {
            _context = new ContextDb();
        }
        public ActionResult Index()
        {
           CourseViewModel courseList = new CourseViewModel
            {
               courses = _context.courses.ToList(),
            };
            return View(courseList);
       
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel admin)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Admines.SingleOrDefault(s => s.Name == admin.Name && s.password == admin.Password);
                if (user == null)
                {
                    ModelState.AddModelError("Name", "Name or Password incorrect");
                    return View(admin);
                }



                ClaimsIdentity identity = new ClaimsIdentity(new[]
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Role,"Admin")
                }, DefaultAuthenticationTypes.ApplicationCookie);

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);


                return RedirectToAction("Index", "Course");
            }
            ModelState.AddModelError("Name", "Name or Password incorrect");

            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }
    }
}