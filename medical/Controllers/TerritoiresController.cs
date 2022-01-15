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
    public class TerritoiresController : Controller
    {
        private readonly ITerritoireService _territoireService;
        private readonly IProvinceService _provinceService;

        public TerritoiresController()
        {
            _territoireService = new TerritoireService();
            _provinceService = new ProvinceService();
        }

        public TerritoiresController(ITerritoireService territoireService, IProvinceService provinceService)
        {
            _territoireService = territoireService;
            _provinceService = provinceService;
        }

        // GET: Territoires
        public ActionResult Index()
        {
            var territoires = from s in _territoireService.GetTerritoire() select s;
            return View(territoires.ToList());
        }

        // GET: Territoires/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territoire territoire = _territoireService.GetTerritoireByID(id);
            if (territoire == null)
            {
                return HttpNotFound();
            }
            return View(territoire);
        }

        // GET: Territoires/Create
        public PartialViewResult Create()
        {
            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION");
            return PartialView();
        }

        // POST: Territoires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTERRITOIRE,IDPROVINCE,DESIGNATION")] Territoire territoire)
        {
            if (ModelState.IsValid)
            {
                _territoireService.InsertTerritoire(territoire);
                _territoireService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION", territoire.IDPROVINCE);
            return View(territoire);
        }

        // GET: Territoires/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territoire territoire = _territoireService.GetTerritoireByID(id);
            if (territoire == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION", territoire.IDPROVINCE);
            return View(territoire);
        }

        // POST: Territoires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTERRITOIRE,IDPROVINCE,DESIGNATION")] Territoire territoire)
        {
            if (ModelState.IsValid)
            {
               _territoireService.UpdateTerritoire(territoire);
                _territoireService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPROVINCE = new SelectList(_provinceService.GetProvince(), "IDPROVINCE", "DESIGNATION", territoire.IDPROVINCE);
            return View(territoire);
        }

        // GET: Territoires/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Territoire territoire = _territoireService.GetTerritoireByID(id);
            if (territoire == null)
            {
                return HttpNotFound();
            }
            return View(territoire);
        }

        // POST: Territoires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Territoire territoire = _territoireService.GetTerritoireByID(id);
            _territoireService.DeleteTerritoire(id);
            _territoireService.Save();
            return RedirectToAction("Index");
        }

    }
}
