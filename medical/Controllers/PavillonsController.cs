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
    public class PavillonsController : Controller
    {
        private readonly IPavillonService _pavillonService;
        private readonly IServiceService _serviceService;

        public PavillonsController()
        {
            _pavillonService = new PavillonService();
            _serviceService = new ServiceService();
        }

        public PavillonsController(IPavillonService pavillonService, IServiceService serviceService)
        {
            _pavillonService = pavillonService;
            _serviceService = serviceService;
        }

        // GET: Pavillons
        public ActionResult Index()
        {
            var pavillons = from s in _pavillonService.GetPavillon() select s;
            return View(pavillons.ToList());
        }

        // GET: Pavillons/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pavillon pavillon = _pavillonService.GetPavillonByID(id);
            if (pavillon == null)
            {
                return HttpNotFound();
            }
            return View(pavillon);
        }

        // GET: Pavillons/Create
        public PartialViewResult Create()
        {
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION");
            return PartialView();
        }

        // POST: Pavillons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPAVILLON,IDSERVICE,DESIGNATION")] Pavillon pavillon)
        {
            if (ModelState.IsValid)
            {
                _pavillonService.InsertPavillon(pavillon);
               _pavillonService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", pavillon.IDSERVICE);
            return View(pavillon);
        }

        // GET: Pavillons/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pavillon pavillon = _pavillonService.GetPavillonByID(id);
            if (pavillon == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", pavillon.IDSERVICE);
            return View(pavillon);
        }

        // POST: Pavillons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPAVILLON,IDSERVICE,DESIGNATION")] Pavillon pavillon)
        {
            if (ModelState.IsValid)
            {
                _pavillonService.UpdatePavillon(pavillon);
                _pavillonService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", pavillon.IDSERVICE);
            return View(pavillon);
        }

        // GET: Pavillons/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pavillon pavillon = _pavillonService.GetPavillonByID(id);
            if (pavillon == null)
            {
                return HttpNotFound();
            }
            return View(pavillon);
        }

        // POST: Pavillons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pavillon pavillon = _pavillonService.GetPavillonByID(id);
            _pavillonService.DeletePavillon(id);
            _pavillonService.Save();
            return RedirectToAction("Index");
        }

    }
}
