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
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;

namespace medical.Controllers
{
    [Authorize]
    public class ActeInfiemiersController : Controller
    {
       
        private readonly IActeInfiemierService _acteInfirmierService;
        private readonly IInfirmierService _infirmierService;
        private readonly IPatientService _patientService;
        public ActeInfiemiersController()
        {
            _acteInfirmierService = new ActeInfiemierService();
            _infirmierService = new InfirmierService();
            _patientService = new PatientService();
        }

        public ActeInfiemiersController(IActeInfiemierService acteInfirmierService, IInfirmierService infirmierService, IPatientService patientService)
        {
            _acteInfirmierService = acteInfirmierService;
            _infirmierService = infirmierService;
            _patientService = patientService;
        }

        // GET: ActeInfiemiers
        public ActionResult Index()
        {
            var item = from s in _acteInfirmierService.GetActeInfiemier() select s;
            return View(item.ToList());
        }

        //Export
        public ActionResult Reports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/ActeInfirmier.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "ActeInfirmierDataSet",
                Value = from s in _acteInfirmierService.GetActeInfiemier()
                        select s
            };

            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string fileNameExtension;

            if (reportType == "Excel")
            {
                fileNameExtension = ".xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = ".docx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = ".pdf";
            }
            else
            {
                fileNameExtension = ".jpg";
            }

            byte[] renderByte;

            renderByte = localReport.Render(reportType, "", out string mimeType, out string encoding, out fileNameExtension, out string[] stream, out Warning[] warnings);
            Response.AddHeader("content-disposition", "attachment;filename=Marklist." + fileNameExtension);
            return File(renderByte, fileNameExtension);
        }
        // GET: ActeInfiemiers/Details/5
        public ActionResult Details(int id)
        {
            ActeInfiemier acte = _acteInfirmierService.GetActeInfiemierByID(id);
            return View(acte);
        }

        // GET: ActeInfiemiers/Create
        public PartialViewResult Create()
        {
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet");
            ViewBag.Id = new SelectList(_infirmierService.GetInfirmier(), "Id", "NomComplet");
            return PartialView();
        }

        // POST: ActeInfiemiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDACTEINFIRMIER,IDPATIENT,ACTE,Id")] ActeInfiemier acteInfiemier)
        {
            if (ModelState.IsValid)
            {
                _acteInfirmierService.InsertActeInfiemier(acteInfiemier);
                _acteInfirmierService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acteInfiemier.IDPATIENT);
            ViewBag.Id = new SelectList(_infirmierService.GetInfirmier(), "Id", "NomComplet", acteInfiemier.Id);
            return View(acteInfiemier);
        }

        // GET: ActeInfiemiers/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActeInfiemier acte = _acteInfirmierService.GetActeInfiemierByID(id);
            if (acte == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acte.IDPATIENT);
            ViewBag.Id = new SelectList(_infirmierService.GetInfirmier(), "Id", "NomComplet", acte.Id);
            return View(acte);
        }

        // POST: ActeInfiemiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDACTEINFIRMIER,IDPATIENT,ACTE,Id")] ActeInfiemier acteInfiemier)
        {
            if (ModelState.IsValid)
            {
                _acteInfirmierService.UpdateActeInfiemier(acteInfiemier);
                _acteInfirmierService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acteInfiemier.IDPATIENT);
            ViewBag.Id = new SelectList(_infirmierService.GetInfirmier(), "Id", "NomComplet", acteInfiemier.Id);
            return View(acteInfiemier);
        }

        // GET: ActeInfiemiers/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActeInfiemier acte = _acteInfirmierService.GetActeInfiemierByID(id);
            if (acte == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", acte.IDPATIENT);
            ViewBag.Id = new SelectList(_infirmierService.GetInfirmier(), "Id", "NomComplet", acte.Id);
            return View(acte);
        }

        // POST: ActeInfiemiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActeInfiemier acte = _acteInfirmierService.GetActeInfiemierByID(id);
            _acteInfirmierService.DeleteActeInfiemier(id);
            _acteInfirmierService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Acte Infirmier
        public PartialViewResult SearchActeInfirmier(string searchString)
        {
            var item = from s in _acteInfirmierService.GetActeInfiemier() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.ACTE.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchActeInfirmier", item.ToList());
        }
    }
}
