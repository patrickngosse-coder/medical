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
    public class ConsultationsController : Controller
    {
        private readonly IConsultationService _consultationService;
        private readonly IMedecinService _medecinService;
        private readonly IPatientService _patientService;

        public ConsultationsController()
        {
            _consultationService = new ConsultationService();
            _medecinService = new MedecinService();
            _patientService = new PatientService();
        }

        public ConsultationsController(IConsultationService consultationService, IMedecinService medecinService, IPatientService patientService)
        {
            _consultationService = consultationService;
            _medecinService = medecinService;
            _patientService = patientService;
        }

        // GET: Consultations
        public ActionResult Index()
        {
            var item = from s in _consultationService.GetConsultation() select s;
            return View(item.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _consultationService.GetConsultation();
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
        public PartialViewResult ConsultationIndex()
        {
            var data = from s in _consultationService.GetConsultation() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        // GET: Consultations/Details/5
        public ActionResult Details(int id)
        {
            Consultation consultation = _consultationService.GetConsultationByID(id);
            return View(consultation);
        }

        // GET: Consultations/Create
        public PartialViewResult Create()
        {
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet");
            ViewBag.PATIENTID = new SelectList(_patientService.GetPatient(), "PATIENTID", "NomComplet");
            return PartialView();
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCONSULTATION,DATECONSULTATION,DECISION,PLAINTE,DIAGNOSTIC,TEMPERATURE,PRESSIONARTERIEL,POIDS,PATIENTID,Id")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                _consultationService.InsertConsultation(consultation);
                _consultationService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", consultation.Id);
            ViewBag.PATIENTID = new SelectList(_patientService.GetPatient(), "PATIENTID", "NomComplet", consultation.PATIENTID);
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = _consultationService.GetConsultationByID(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", consultation.Id);
            ViewBag.PATIENTID = new SelectList(_patientService.GetPatient(), "PATIENTID", "NomComplet", consultation.PATIENTID);
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCONSULTATION,DATECONSULTATION,DECISION,PLAINTE,DIAGNOSTIC,TEMPERATURE,PRESSIONARTERIEL,POIDS,PATIENTID,Id")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                _consultationService.UpdateConsultation(consultation);
                _consultationService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", consultation.Id);
            ViewBag.PATIENTID = new SelectList(_patientService.GetPatient(), "PATIENTID", "NomComplet", consultation.PATIENTID);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = _consultationService.GetConsultationByID(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", consultation.Id);
            ViewBag.PATIENTID = new SelectList(_patientService.GetPatient(), "PATIENTID", "NomComplet", consultation.PATIENTID);
            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultation consultation = _consultationService.GetConsultationByID(id);
            _consultationService.DeleteConsultation(id);
            _consultationService.Save();
            return RedirectToAction("Index");
        }

    }
}
