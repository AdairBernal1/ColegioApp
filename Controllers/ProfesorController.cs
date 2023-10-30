using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ColegioApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioApp.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly ColegioContext db;

        public ProfesorController(ColegioContext context)
        {
            db = context;
        }

        public ActionResult ProfesorIndex()
        {
            var profesores = db.Profesores.ToList();

            return View("ProfesorIndex", profesores);
        }

        public ActionResult ProfesorCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfesorCreate(Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                db.Profesores.Add(profesor);
                db.SaveChanges();
                return RedirectToAction("ProfesorIndex");
            }
            return View("ProfesorCreate", profesor);
        }

        public ActionResult ProfesorUpdate(int id)
        {
            var profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return NotFound();
            }
            return View("ProfesorCreate", profesor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProfesorUpdate(Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProfesorIndex");
            }
            return View("ProfesorUpdate", profesor);
        }

        public ActionResult ProfesorDelete(int id)
        {
            var profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return NotFound();
            }
            return View("ProfesorDelete", profesor);
        }

        [HttpPost]
        [ActionName("ProfesorDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ProfesorDeleteConfirmed(int id)
        {
            var profesor = db.Profesores.Find(id);
            db.Profesores.Remove(profesor);
            db.SaveChanges();
            return RedirectToAction("ProfesorIndex");
        }
    }
}
