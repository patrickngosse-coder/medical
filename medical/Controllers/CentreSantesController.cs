using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using medical.Data;
using medical.Model.Models;
using medical.Service.Services;

namespace medical.Controllers
{
    [Authorize]
    public class CentreSantesController : Controller
    {
        private readonly ICentreSanteService _centreSante;
        private readonly IZoneSanteService _zoneSante;

        public CentreSantesController()
        {
            _centreSante = new CentreSanteService();
            _zoneSante = new ZoneSanteService();
        }

        public CentreSantesController(ICentreSanteService centreSante, IZoneSanteService zoneSante)
        {
            _centreSante = centreSante;
            _zoneSante = zoneSante;
        }

        // GET: CentreSantes
        public ActionResult Index()
        {
            var centreSantes = from s in  _centreSante.GetCentreSante() select s;
            return View(centreSantes.ToList());
        }

        // GET: CentreSantes/Details/5
        public ActionResult Details(int id)
        {
            CentreSante centresante = _centreSante.GetCentreSanteByID(id);
            return View(centresante);
        }

        // GET: CentreSantes/Create
        public PartialViewResult Create()
        {
            ViewBag.IDZONE = new SelectList(_zoneSante.GetZoneSante(), "IDZONE", "DESIGNATION");
            return PartialView();
        }

        // POST: CentreSantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCENTRE,IDZONE,DESIGNATION")] CentreSante centreSante)
        {
            if (ModelState.IsValid)
            {
                _centreSante.InsertCentreSante(centreSante);
                _centreSante.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDZONE = new SelectList(_zoneSante.GetZoneSante(), "IDZONE", "DESIGNATION", centreSante.IDZONE);
            return View(centreSante);
        }

        // GET: CentreSantes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentreSante centreSante = _centreSante.GetCentreSanteByID(id);
            if (centreSante == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDZONE = new SelectList(_zoneSante.GetZoneSante(), "IDZONE", "DESIGNATION", centreSante.IDZONE);
            return View(centreSante);
        }

        // POST: CentreSantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCENTRE,IDZONE,DESIGNATION")] CentreSante centreSante)
        {
            if (ModelState.IsValid)
            {
                _centreSante.UpdateCentreSante(centreSante);
                _centreSante.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDZONE = new SelectList(_zoneSante.GetZoneSante(), "IDZONE", "DESIGNATION", centreSante.IDZONE);
            return View(centreSante);
        }

        // GET: CentreSantes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentreSante centreSante = _centreSante.GetCentreSanteByID(id);
            if (centreSante == null)
            {
                return HttpNotFound();
            }
            return View(centreSante);
        }

        // POST: CentreSantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CentreSante centreSante = _centreSante.GetCentreSanteByID(id);
           _centreSante.DeleteCentreSante(id);
            _centreSante.Save();
            return RedirectToAction("Index");
        }

      
    }
}
