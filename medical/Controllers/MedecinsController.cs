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
    public class MedecinsController : Controller
    {
        private readonly IMedecinService _medecinService;
        private readonly IServiceService _serviceService;

        public MedecinsController()
        {
            _medecinService = new MedecinService();
            _serviceService = new ServiceService();
        }

        // GET: Medecins
        public ActionResult Index()
        {
            var users = from s in _medecinService.GetMedecin() select s;
            return View(users.ToList());
        }

        //Export
        public ActionResult Reports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/Medecin.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "MedecinDataSet",
                Value = from s in _medecinService.GetMedecin()
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

        //Liste
        public PartialViewResult MedecinIndex()
        {
            var data = from s in _medecinService.GetMedecin() select s;
            return PartialView("_PartialIndex", data.ToList());
        }
        // GET: Medecins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = _medecinService.GetMedecinByID(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // GET: Medecins/Create
        public ActionResult Create()
        {
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION");
            return View();
        }

        // POST: Medecins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NOM,POSTNOM,PRENOM,SEXE,LIEUNAISSANCE,DATENAISSANCE,ETATCIVIL,MATRICULE,GRADE,IDSERVICE,ADRESSSE,IDHOPITAL")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
                _medecinService.InsertMedecin(medecin);
               _medecinService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", medecin.IDSERVICE);
            return View(medecin);
        }

        // GET: Medecins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = _medecinService.GetMedecinByID(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", medecin.IDSERVICE);
            return View(medecin);
        }

        // POST: Medecins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NOM,POSTNOM,PRENOM,SEXE,LIEUNAISSANCE,DATENAISSANCE,ETATCIVIL,MATRICULE,GRADE,IDSERVICE,ADRESSSE,IDHOPITAL")] Medecin medecin)
        {
            if (ModelState.IsValid)
            {
               _medecinService.UpdateMedecin(medecin);
                _medecinService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", medecin.IDSERVICE);
            return View(medecin);
        }

        // GET: Medecins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medecin medecin = _medecinService.GetMedecinByID(id);
            if (medecin == null)
            {
                return HttpNotFound();
            }
            return View(medecin);
        }

        // POST: Medecins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Medecin medecin = _medecinService.GetMedecinByID(id);
           _medecinService.DeleteMedecin(id);
            _medecinService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Medecin
        public PartialViewResult SearchMedecin(string searchString)
        {
            var item = from s in _medecinService.GetMedecin() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.NOM.ToUpper().Contains(searchString.ToUpper()) || s.POSTNOM.ToUpper().Contains(searchString.ToUpper()) || s.PRENOM.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchMedecin", item.ToList());
        }
    }
}
