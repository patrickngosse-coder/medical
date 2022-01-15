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
    public class VillesController : Controller
    {
        private readonly IVilleService _villeService;
        private readonly ITerritoireService _territoireService;

        public VillesController()
        {
            _villeService = new VilleService();
            _territoireService = new TerritoireService();
        }

        public VillesController(IVilleService villeService, ITerritoireService territoireService)
        {
            _villeService = villeService;
            _territoireService = territoireService;
        }

        // GET: Villes
        public ActionResult Index()
        {
            var villes = from s in _villeService.GetVille() select s;
            return View(villes.ToList());
        }

        // GET: Villes/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville =_villeService.GetVilleByID(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // GET: Villes/Create
        public PartialViewResult Create()
        {
            ViewBag.IDTERRITOIRE = new SelectList(_territoireService.GetTerritoire(), "IDTERRITOIRE", "DESIGNATION");
            return PartialView();
        }

        // POST: Villes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVILLE,IDTERRITOIRE,DESIGNATION")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                _villeService.InsertVille(ville);
                _villeService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDTERRITOIRE = new SelectList(_territoireService.GetTerritoire(), "IDTERRITOIRE", "DESIGNATION", ville.IDTERRITOIRE);
            return View(ville);
        }

        // GET: Villes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = _villeService.GetVilleByID(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTERRITOIRE = new SelectList(_territoireService.GetTerritoire(), "IDTERRITOIRE", "DESIGNATION", ville.IDTERRITOIRE);
            return View(ville);
        }

        // POST: Villes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDVILLE,IDTERRITOIRE,DESIGNATION")] Ville ville)
        {
            if (ModelState.IsValid)
            {
                _villeService.UpdateVille(ville);
                _villeService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDTERRITOIRE = new SelectList(_territoireService.GetTerritoire(), "IDTERRITOIRE", "DESIGNATION", ville.IDTERRITOIRE);
            return View(ville);
        }

        // GET: Villes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ville ville = _villeService.GetVilleByID(id);
            if (ville == null)
            {
                return HttpNotFound();
            }
            return View(ville);
        }

        // POST: Villes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ville ville = _villeService.GetVilleByID(id);
            _villeService.DeleteVille(id);
            _villeService.Save();
            return RedirectToAction("Index");
        }

    }
}
