using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ColegioApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioApp.Controllers
{
    public class GradoController : Controller
    {
        private readonly ColegioContext db;

        public GradoController(ColegioContext context)
        {
            db = context;
        }

        public ActionResult GradoIndex()
        {
            var grados = db.Grados
                .Include(g => g.Profesor) // Include related Profesor
                .ToList();

            return View("GradoIndex", grados);
        }

        public ActionResult GradoCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GradoCreate(Grado grado)
        {
            if (ModelState.IsValid)
            {
                db.Grados.Add(grado);
                db.SaveChanges();
                return RedirectToAction("GradoIndex");
            }
            return View("GradoCreate", grado);
        }

        public ActionResult GradoUpdate(int id)
        {
            var grado = db.Grados.Find(id);
            if (grado == null)
            {
                return NotFound();
            }
            return View("GradoCreate", grado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GradoUpdate(Grado grado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GradoIndex");
            }
            return View("GradoUpdate", grado);
        }

        public ActionResult GradoDelete(int id)
        {
            var grado = db.Grados.Find(id);
            if (grado == null)
            {
                return NotFound();
            }
            return View("GradoDelete", grado);
        }

        [HttpPost]
        [ActionName("GradoDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GradoDeleteConfirmed(int id)
        {
            var grado = db.Grados.Find(id);
            db.Grados.Remove(grado);
            db.SaveChanges();
            return RedirectToAction("GradoIndex");
        }
    }
}
