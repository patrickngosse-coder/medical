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
    public class HopitalsController : Controller
    {
        private readonly IHopitalService _hopitalService;
        private readonly IVilleService _villeService;

        public HopitalsController()
        {
            _hopitalService = new HopitalService();
            _villeService = new VilleService();
        }

        public HopitalsController(IHopitalService hopitalService, IVilleService villeService)
        {
            _hopitalService = hopitalService;
            _villeService = villeService;
        }

        // GET: Hopitals
        public ActionResult Index()
        {
            var hopitals = from s in _hopitalService.GetHopital() select s;
            return View(hopitals.ToList());
        }

        // GET: Hopitals/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hopital hopital = _hopitalService.GetHopitalByID(id);
            if (hopital == null)
            {
                return HttpNotFound();
            }
            return View(hopital);
        }

        // GET: Hopitals/Create
        public PartialViewResult Create()
        {
            ViewBag.IDVILLE = new SelectList(_villeService.GetVille(), "IDVILLE", "DESIGNATION");
            return PartialView();
        }

        // POST: Hopitals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHOPITAL,IDVILLE,COORDONNE,DESIGNATION,StatutHopital,ACTEJURIDIQUE")] Hopital hopital)
        {
            if (ModelState.IsValid) 
            {
                _hopitalService.InsertHopital(hopital);
                _hopitalService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDVILLE = new SelectList(_villeService.GetVille(), "IDVILLE", "DESIGNATION", hopital.IDVILLE);
            return View(hopital);
        }

        // GET: Hopitals/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hopital hopital = _hopitalService.GetHopitalByID(id);
            if (hopital == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDVILLE = new SelectList(_villeService.GetVille(), "IDVILLE", "DESIGNATION", hopital.IDVILLE);
            return View(hopital);
        }

        // POST: Hopitals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHOPITAL,IDVILLE,COORDONNE,DESIGNATION,StatutHopital,ACTEJURIDIQUE")] Hopital hopital)
        {
            if (ModelState.IsValid)
            {
                _hopitalService.UpdateHopital(hopital);
                _hopitalService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDVILLE = new SelectList(_villeService.GetVille(), "IDVILLE", "DESIGNATION", hopital.IDVILLE);
            return View(hopital);
        }

        // GET: Hopitals/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hopital hopital = _hopitalService.GetHopitalByID(id);
            if (hopital == null)
            {
                return HttpNotFound();
            }
            return View(hopital);
        }

        // POST: Hopitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hopital hopital = _hopitalService.GetHopitalByID(id);
            _hopitalService.DeleteHopital(id);
            _hopitalService.Save();
            return RedirectToAction("Index");
        }

    }
}
