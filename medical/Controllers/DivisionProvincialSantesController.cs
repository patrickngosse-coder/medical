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
    public class DivisionProvincialSantesController : Controller
    {
        private readonly IDivisionProvincialSanteService _divisionProvincialSanteService;
        private readonly IProvinceService _provinceService;

        public DivisionProvincialSantesController()
        {
            _divisionProvincialSanteService = new DivisionProvincialSanteService();
            _provinceService = new ProvinceService();
        }

        public DivisionProvincialSantesController(IDivisionProvincialSanteService divisionProvincialSanteService, IProvinceService provinceService)
        {
            _divisionProvincialSanteService = divisionProvincialSanteService;
            _provinceService = provinceService;
        }

        // GET: DivisionProvincialSantes
        public ActionResult Index()
        {
            var divisionProvincialSantes = from s in _divisionProvincialSanteService.GetDivisionProvincialSante() select s;
            return View(divisionProvincialSantes.ToList());
        }

        // GET: DivisionProvincialSantes/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionProvincialSante divisionProvincialSante = _divisionProvincialSanteService.GetDivisionProvincialSanteByID(id);
            if (divisionProvincialSante == null)
            {
                return HttpNotFound();
            }
            return View(divisionProvincialSante);
        }

        // GET: DivisionProvincialSantes/Create
        public PartialViewResult Create()
        {
            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION");
            return PartialView();
        }

        // POST: DivisionProvincialSantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDIVISION,IDPROVINCE,DESIGNATION")] DivisionProvincialSante divisionProvincialSante)
        {
            if (ModelState.IsValid)
            {
                _divisionProvincialSanteService.InsertDivisionProvincialSante(divisionProvincialSante);
                _divisionProvincialSanteService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION", divisionProvincialSante.IDPROVINCE);
            return View(divisionProvincialSante);
        }

        // GET: DivisionProvincialSantes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionProvincialSante divisionProvincialSante = _divisionProvincialSanteService.GetDivisionProvincialSanteByID(id);
            if (divisionProvincialSante == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION", divisionProvincialSante.IDPROVINCE);
            return View(divisionProvincialSante);
        }

        // POST: DivisionProvincialSantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDIVISION,IDPROVINCE,DESIGNATION")] DivisionProvincialSante divisionProvincialSante)
        {
            if (ModelState.IsValid)
            {
                _divisionProvincialSanteService.UpdateDivisionProvincialSante(divisionProvincialSante);
                _divisionProvincialSanteService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION", divisionProvincialSante.IDPROVINCE);
            return View(divisionProvincialSante);
        }

        // GET: DivisionProvincialSantes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DivisionProvincialSante divisionProvincialSante = _divisionProvincialSanteService.GetDivisionProvincialSanteByID(id);
            if (divisionProvincialSante == null)
            {
                return HttpNotFound();
            }
            return View(divisionProvincialSante);
        }

        // POST: DivisionProvincialSantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DivisionProvincialSante divisionProvincialSante = _divisionProvincialSanteService.GetDivisionProvincialSanteByID(id);
            _divisionProvincialSanteService.DeleteDivisionProvincialSante(id);
            _divisionProvincialSanteService.Save();
            return RedirectToAction("Index");
        }

    }
}
