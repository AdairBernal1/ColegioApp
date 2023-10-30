using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ColegioApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioApp.Controllers
{
    public class AlumnoGradoController : Controller
    {
        private readonly ColegioContext db;

        public AlumnoGradoController(ColegioContext context)
        {
            db = context;
        }

        public ActionResult AlumnoGradoIndex()
        {
            var alumnoGrados = db.AlumnoGrados
                .Include(ag => ag.Alumno)
                .Include(ag => ag.Grado)
                .ToList();

            return View("AlumnoGradoIndex", alumnoGrados);
        }

        public ActionResult AlumnoGradoCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlumnoGradoCreate(AlumnoGrado alumnoGrado)
        {
            if (ModelState.IsValid)
            {
                db.AlumnoGrados.Add(alumnoGrado);
                db.SaveChanges();
                return RedirectToAction("AlumnoGradoIndex");
            }
            return View("AlumnoGradoCreate", alumnoGrado);
        }

        public ActionResult AlumnoGradoUpdate(int id)
        {
            var alumnoGrado = db.AlumnoGrados.Find(id);
            if (alumnoGrado == null)
            {
                return NotFound();
            }
            return View("AlumnoGradoCreate", alumnoGrado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlumnoGradoUpdate(AlumnoGrado alumnoGrado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumnoGrado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AlumnoGradoIndex");
            }
            return View("AlumnoGradoUpdate", alumnoGrado);
        }

        public ActionResult AlumnoGradoDelete(int id)
        {
            var alumnoGrado = db.AlumnoGrados.Find(id);
            if (alumnoGrado == null)
            {
                return NotFound();
            }
            return View("AlumnoGradoDelete", alumnoGrado);
        }

        [HttpPost]
        [ActionName("AlumnoGradoDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult AlumnoGradoDeleteConfirmed(int id)
        {
            var alumnoGrado = db.AlumnoGrados.Find(id);
            db.AlumnoGrados.Remove(alumnoGrado);
            db.SaveChanges();
            return RedirectToAction("AlumnoGradoIndex");
        }
    }
}
