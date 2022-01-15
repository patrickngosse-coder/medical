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
    public class MaladiesController : Controller
    {
        private readonly IMaladieService _maladieService;
        private readonly ICentreSanteService _centreSanteService;
        private readonly IHopitalService _hopitalService;

        public MaladiesController()
        {
            _maladieService = new MaladieService();
            _centreSanteService = new CentreSanteService();
            _hopitalService = new HopitalService();
        }

        public MaladiesController(IMaladieService maladieService, ICentreSanteService centreSanteService, IHopitalService hopitalService)
        {
            _maladieService = maladieService;
            _centreSanteService = centreSanteService;
            _hopitalService = hopitalService;
        }

        // GET: Maladies
        public ActionResult Index()
        {
            var maladies = from s in _maladieService.GetMaladie() select s;
            return View(maladies.ToList());
        }

        // GET: Maladies/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maladie maladie = _maladieService.GetMaladieByID(id);
            if (maladie == null)
            {
                return HttpNotFound();
            }
            return View(maladie);
        }

        // GET: Maladies/Create
        public PartialViewResult Create()
        {
            ViewBag.IDCENTRE = new SelectList(_centreSanteService.GetCentreSante(), "IDCENTRE", "DESIGNATION");
            ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE");
            return PartialView();
        }

        // POST: Maladies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDMALADIE,IDPATIENT,IDHOPITAL,IDCENTRE,SYMPTOME,DESIGNATION,OBSERVATION,Jour,Mois,Annee")] Maladie maladie)
        {
            if (ModelState.IsValid)
            {
                _maladieService.InsertMaladie(maladie);
                _maladieService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDCENTRE = new SelectList(_centreSanteService.GetCentreSante(), "IDCENTRE", "DESIGNATION", maladie.IDCENTRE);
            ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE", maladie.IDHOPITAL);
            return View(maladie);
        }

        // GET: Maladies/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maladie maladie = _maladieService.GetMaladieByID(id);
            if (maladie == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCENTRE = new SelectList(_centreSanteService.GetCentreSante(), "IDCENTRE", "DESIGNATION", maladie.IDCENTRE);
            ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE", maladie.IDHOPITAL);
            return View(maladie);
        }

        // POST: Maladies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDMALADIE,IDPATIENT,IDHOPITAL,IDCENTRE,SYMPTOME,DESIGNATION,OBSERVATION,Jour,Mois,Annee")] Maladie maladie)
        {
            if (ModelState.IsValid)
            {
                _maladieService.UpdateMaladie(maladie);
                _maladieService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDCENTRE = new SelectList(_centreSanteService.GetCentreSante(), "IDCENTRE", "DESIGNATION", maladie.IDCENTRE);
            ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE", maladie.IDHOPITAL);
            return View(maladie);
        }

        // GET: Maladies/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maladie maladie = _maladieService.GetMaladieByID(id);
            if (maladie == null)
            {
                return HttpNotFound();
            }
            return View(maladie);
        }

        // POST: Maladies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maladie maladie = _maladieService.GetMaladieByID(id);
            _maladieService.DeleteMaladie(id);
            _maladieService.Save();
            return RedirectToAction("Index");
        }

    }
}
