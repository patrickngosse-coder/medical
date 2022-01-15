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
    public class ZoneSantesController : Controller
    {
        private readonly IZoneSanteService _zoneSanteService;
        private readonly IDivisionProvincialSanteService _divisionProvincialSanteService;

        public ZoneSantesController()
        {
            _zoneSanteService = new ZoneSanteService();
            _divisionProvincialSanteService = new DivisionProvincialSanteService();
        }

        public ZoneSantesController(IZoneSanteService zoneSanteService, IDivisionProvincialSanteService divisionProvincialSanteService)
        {
            _zoneSanteService = zoneSanteService;
            _divisionProvincialSanteService = divisionProvincialSanteService;
        }

        // GET: ZoneSantes
        public ActionResult Index()
        {
            var zoneSantes = from s in _zoneSanteService.GetZoneSante() select s;
            return View(zoneSantes.ToList());
        }

        // GET: ZoneSantes/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneSante zoneSante = _zoneSanteService.GetZoneSanteByID(id);
            if (zoneSante == null)
            {
                return HttpNotFound();
            }
            return View(zoneSante);
        }

        // GET: ZoneSantes/Create
        public PartialViewResult Create()
        {
            ViewBag.IDDIVISION = new SelectList(_divisionProvincialSanteService.GetDivisionProvincialSante(), "IDDIVISION", "DESIGNATION");
            return PartialView();
        }

        // POST: ZoneSantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDZONE,IDDIVISION,DESIGNATION")] ZoneSante zoneSante)
        {
            if (ModelState.IsValid)
            {
                _zoneSanteService.InsertZoneSante(zoneSante);
                _zoneSanteService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDDIVISION = new SelectList(_divisionProvincialSanteService.GetDivisionProvincialSante(), "IDDIVISION", "DESIGNATION", zoneSante.IDDIVISION);
            return View(zoneSante);
        }

        // GET: ZoneSantes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneSante zoneSante = _zoneSanteService.GetZoneSanteByID(id);
            if (zoneSante == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDIVISION = new SelectList(_divisionProvincialSanteService.GetDivisionProvincialSante(), "IDDIVISION", "DESIGNATION", zoneSante.IDDIVISION);
            return View(zoneSante);
        }

        // POST: ZoneSantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDZONE,IDDIVISION,DESIGNATION")] ZoneSante zoneSante)
        {
            if (ModelState.IsValid)
            {
                _zoneSanteService.UpdateZoneSante(zoneSante);
                _zoneSanteService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDDIVISION = new SelectList(_divisionProvincialSanteService.GetDivisionProvincialSante(), "IDDIVISION", "DESIGNATION", zoneSante.IDDIVISION);
            return View(zoneSante);
        }

        // GET: ZoneSantes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZoneSante zoneSante = _zoneSanteService.GetZoneSanteByID(id);
            if (zoneSante == null)
            {
                return HttpNotFound();
            }
            return View(zoneSante);
        }

        // POST: ZoneSantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ZoneSante zoneSante = _zoneSanteService.GetZoneSanteByID(id);
            _zoneSanteService.DeleteZoneSante(id);
            _zoneSanteService.Save();
            return RedirectToAction("Index");
        }

    }
}
