using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using medical.Data;
using medical.Model.Models;
using medical.Service.Services;

namespace medical.Controllers
{
    [Authorize]
    public class RendezvousController : Controller
    {
        public readonly IRendezvousService _rendezvousService;
        public readonly IPatientService _patientService;
        public readonly IMedecinService _medecinService;

        public RendezvousController()
        {
            _rendezvousService = new RendezvousService();
            _patientService = new PatientService();
            _medecinService = new MedecinService();
        }

        public RendezvousController(IRendezvousService rendezvousService, IPatientService patientService, IMedecinService medecinService)
        {
            _rendezvousService = rendezvousService;
            _patientService = patientService;
            _medecinService = medecinService;
        }

        // GET: Rendezvous
        public ActionResult Index()
        {
            var item = from s in _rendezvousService.GetRendezvous() select s;
            return View(item.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _rendezvousService.GetRendezvous();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";

            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index");

        }

        // GET: Rendezvous/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendezvous rendezvous = _rendezvousService.GetRendezvousByID(id);
            if (rendezvous == null)
            {
                return HttpNotFound();
            }
            return View(rendezvous);
        }

        // GET: Rendezvous/Create
        public PartialViewResult Create()
        {
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NOM");
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NOM");
            return PartialView();
        }

        // POST: Rendezvous/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDRDV,IDPATIENT,Id,DATERENDEZVOUS,NOTE")] Rendezvous rendezvous)
        {
            if (ModelState.IsValid)
            {
               _rendezvousService.InsertRendezvous(rendezvous);
                _rendezvousService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NOM", rendezvous.Id);
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NOM", rendezvous.IDPATIENT);
            return View(rendezvous);
        }

        // GET: Rendezvous/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendezvous rendezvous = _rendezvousService.GetRendezvousByID(id);
            if (rendezvous == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NOM");
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NOM");
            return View(rendezvous);
        }

        // POST: Rendezvous/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDRDV,IDPATIENT,Id,DATERENDEZVOUS,NOTE")] Rendezvous rendezvous)
        {
            if (ModelState.IsValid)
            {
                _rendezvousService.UpdateRendezvous(rendezvous);
                _rendezvousService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NOM", rendezvous.Id);
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NOM", rendezvous.IDPATIENT);
            return View(rendezvous);
        }

        // GET: Rendezvous/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rendezvous rendezvous = _rendezvousService.GetRendezvousByID(id);
            if (rendezvous == null)
            {
                return HttpNotFound();
            }
            return View(rendezvous);
        }

        // POST: Rendezvous/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rendezvous rendezvous = _rendezvousService.GetRendezvousByID(id);
            _rendezvousService.DeleteRendezvous(id);
           _rendezvousService.Save();
            return RedirectToAction("Index");
        }

    }
}
