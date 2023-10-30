using Microsoft.EntityFrameworkCore;
using System.Linq;
using ColegioApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColegioApp.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly ColegioContext db;

        public AlumnoController(ColegioContext context)
        {
            db = context;
        }

        public ActionResult AlumnoIndex()
        {
            var alumnos = db.Alumnos.ToList();
            return View("AlumnoIndex", alumnos);
        }

        public ActionResult AlumnoCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlumnoCreate(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Alumnos.Add(alumno);
                db.SaveChanges();
                return RedirectToAction("AlumnoIndex");
            }
            return View("AlumnoCreate", alumno);
        }

        public ActionResult AlumnoUpdate(int id)
        {
            var alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View("AlumnoCreate", alumno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlumnoUpdate(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AlumnoIndex");
            }
            return View("AlumnoUpdate", alumno);
        }

        public ActionResult AlumnoDelete(int id)
        {
            var alumno = db.Alumnos.Find(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View("AlumnoDelete", alumno);
        }

        [HttpPost, ActionName("AlumnoDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AlumnoDeleteConfirmed(int id)
        {
            var alumno = db.Alumnos.Find(id);
            db.Alumnos.Remove(alumno);
            db.SaveChanges();
            return RedirectToAction("AlumnoIndex");
        }
    }
}
