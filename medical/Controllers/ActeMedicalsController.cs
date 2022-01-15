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
    public class ActeMedicalsController : Controller
    {
        private readonly IActeMedicalService _acteMedicalService;
        private readonly IMedecinService _medecinService;
        private readonly IPatientService _patientService;

        public ActeMedicalsController()
        {
            _acteMedicalService = new ActeMedicalService();
            _medecinService = new MedecinService();
            _patientService = new PatientService();
        }
        public ActeMedicalsController(IActeMedicalService acteMedicalService, IMedecinService medecinService, IPatientService patientService)
        {
            _acteMedicalService = acteMedicalService;
            _medecinService = medecinService;
            _patientService = patientService;
        }

        // GET: ActeMedicals
        public ActionResult Index()
        {
            var item = from s in _acteMedicalService.GetActeMedical() select s;
            return View(item.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _acteMedicalService.GetActeMedical();
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

        //Liste
        public PartialViewResult ActeMedicalIndex()
        {
            var data = from s in _acteMedicalService.GetActeMedical() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        // GET: ActeMedicals/Details/5
        public ActionResult Details(int id)
        {
            ActeMedical acte = _acteMedicalService.GetActeMedicalByID(id);
            return View(acte);
        }

        // GET: ActeMedicals/Create
        public PartialViewResult Create()
        {
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet");
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet");
            return PartialView();
        }

        // POST: ActeMedicals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDACTEMEDICAUX,IDPATIENT,ACTE,Id")] ActeMedical acteMedical)
        {
            if (ModelState.IsValid)
            {
                _acteMedicalService.InsertActeMedical(acteMedical);
                _acteMedicalService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acteMedical.IDPATIENT);
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", acteMedical.Id);
            return View(acteMedical);
        }

        // GET: ActeMedicals/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActeMedical acte = _acteMedicalService.GetActeMedicalByID(id);
            if (acte == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acte.IDPATIENT);
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", acte.Id);
            return View(acte);
        }

        // POST: ActeMedicals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDACTEMEDICAUX,IDPATIENT,ACTE,Id")] ActeMedical acteMedical)
        {
            if (ModelState.IsValid)
            {
                _acteMedicalService.UpdateActeMedical(acteMedical);
                _acteMedicalService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acteMedical.IDPATIENT);
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", acteMedical.Id);
            return View(acteMedical);
        }

        // GET: ActeMedicals/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActeMedical acte = _acteMedicalService.GetActeMedicalByID(id);
            if (acte == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acte.IDPATIENT);
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", acte.Id);
            return View(acte);
        }

        // POST: ActeMedicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActeMedical acteMedical = _acteMedicalService.GetActeMedicalByID(id); ;
            _acteMedicalService.DeleteActeMedical(id);
            _acteMedicalService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Acte Medical
        public PartialViewResult SearchActeMedical(string searchString)
        {
            var item = from s in _acteMedicalService.GetActeMedical() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.ACTE.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchActeMedical", item.ToList());
        }
    }
}
