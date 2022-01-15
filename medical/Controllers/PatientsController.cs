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
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientsController()
        {
            _patientService = new PatientService();
        }

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patients
        public ActionResult Index()
        {
            var item = from s in _patientService.GetPatient() select s;
            return View(item);
        }

        //Liste
        public PartialViewResult PatientIndex()
        {
            var data = from s in _patientService.GetPatient() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _patientService.GetPatient();
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

        // GET: Patients/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = _patientService.GetPatientByID(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PATIENT,NOM,POSTNOM,PRENOM,SEXE,LIEUNAISSANCE,DATENAISSANCE,ETATCIVIL,TEL,ADRESSSE")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.InsertPatient(patient);
                _patientService.Save();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = _patientService.GetPatientByID(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PATIENT,NOM,POSTNOM,PRENOM,SEXE,LIEUNAISSANCE,DATENAISSANCE,ETATCIVIL,TEL,ADRESSSE")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.UpdatePatient(patient);
                _patientService.Save();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = _patientService.GetPatientByID(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = _patientService.GetPatientByID(id);
            _patientService.DeletePatient(id);
            _patientService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Patient
        public PartialViewResult SearchPatient(string searchString)
        {
            var item = from s in _patientService.GetPatient() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.NOM.ToUpper().Contains(searchString.ToUpper()) || s.POSTNOM.ToUpper().Contains(searchString.ToUpper()) || s.PRENOM.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchPatient", item.ToList());
        }
    }
}
