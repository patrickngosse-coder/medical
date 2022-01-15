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
    public class HospitalisationsController : Controller
    {
        private readonly IHospitalisationService _hospitalisationService;
        private readonly IPavillonService _pavillonService;
        private readonly IPatientService _patientService;
        public HospitalisationsController()
        {
            _hospitalisationService = new HospitalisationService();
            _patientService = new PatientService();
            _pavillonService = new PavillonService();
        }

        public HospitalisationsController(IHospitalisationService hospitalisationService, IPavillonService pavillonService, IPatientService patientService)
        {
            _hospitalisationService = hospitalisationService;
            _patientService = patientService;
            _pavillonService = pavillonService;
        }

        // GET: Hospitalisations
        public ActionResult Index()
        {
            var hospitalisations = from s in _hospitalisationService.GetHospitalisation() select s;
            return View(hospitalisations.ToList());
        }

        //Liste
        public PartialViewResult HospitalisationIndex()
        {
            var data = from s in _hospitalisationService.GetHospitalisation() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _hospitalisationService.GetHospitalisation();
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

        // GET: Hospitalisations/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitalisation hospitalisation = _hospitalisationService.GetHospitalisationByID(id);
            if (hospitalisation == null)
            {
                return HttpNotFound();
            }
            return View(hospitalisation);
        }

        // GET: Hospitalisations/Create
        public PartialViewResult Create()
        {
            ViewBag.IDPAVILLON = new SelectList(_pavillonService.GetPavillon(), "IDPAVILLON", "DESIGNATION");
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet");
            return PartialView();
        }

        // POST: Hospitalisations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHOSPI,IDPAVILLON,IDPATIENT,NBRJOUR,PRIXUNITAIRE")] Hospitalisation hospitalisation)
        {
            if (ModelState.IsValid)
            {
                _hospitalisationService.InsertHospitalisation(hospitalisation);
                _hospitalisationService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDPAVILLON = new SelectList(_pavillonService.GetPavillon(), "IDPAVILLON", "DESIGNATION", hospitalisation.IDPAVILLON);
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", hospitalisation.IDPATIENT);
            return View(hospitalisation);
        }

        // GET: Hospitalisations/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitalisation hospitalisation = _hospitalisationService.GetHospitalisationByID(id);
            if (hospitalisation == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPAVILLON = new SelectList(_pavillonService.GetPavillon(), "IDPAVILLON", "DESIGNATION", hospitalisation.IDPAVILLON);
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", hospitalisation.IDPATIENT);
            return View(hospitalisation);
        }

        // POST: Hospitalisations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHOSPI,IDPAVILLON,IDPATIENT,NBRJOUR,PRIXUNITAIRE")] Hospitalisation hospitalisation)
        {
            if (ModelState.IsValid)
            {
                _hospitalisationService.UpdateHospitalisation(hospitalisation);
                _hospitalisationService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPAVILLON = new SelectList(_pavillonService.GetPavillon(), "IDPAVILLON", "DESIGNATION", hospitalisation.IDPAVILLON);
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", hospitalisation.IDPATIENT);
            return View(hospitalisation);
        }

        // GET: Hospitalisations/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hospitalisation hospitalisation = _hospitalisationService.GetHospitalisationByID(id);
            if (hospitalisation == null)
            {
                return HttpNotFound();
            }
            return View(hospitalisation);
        }

        // POST: Hospitalisations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hospitalisation hospitalisation = _hospitalisationService.GetHospitalisationByID(id);
            _hospitalisationService.DeleteHospitalisation(id);
            _hospitalisationService.Save();
            return RedirectToAction("Index");
        }

    }
}
