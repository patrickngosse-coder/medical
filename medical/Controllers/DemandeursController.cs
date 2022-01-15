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
    public class DemandeursController : Controller
    {
        private readonly IDemandeurService _demandeurService;

        public DemandeursController()
        {
            _demandeurService = new DemandeurService();
        }

        // GET: Demandeurs
        public ActionResult Index()
        {
            var item = from s in _demandeurService.GetDemandeur() select s;
            return View(item.ToList());
        }

        //Export
        public ActionResult Reports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/Demandeur.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "DemandeurDataSet",
                Value = from s in _demandeurService.GetDemandeur()
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
        public PartialViewResult DemandeurIndex()
        {
            var data = from s in _demandeurService.GetDemandeur() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        // GET: Demandeurs/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandeur demandeur = _demandeurService.GetDemandeurByID(id);
            if (demandeur == null)
            {
                return HttpNotFound();
            }
            return View(demandeur);
        }

        // GET: Demandeurs/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Demandeurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDDEMANDEUR,NOM,POSTNOM,PRENOM,SEXE")] Demandeur demandeur)
        {
            if (ModelState.IsValid)
            {
                _demandeurService.InsertDemandeur(demandeur);
                _demandeurService.Save();
                return RedirectToAction("Index");
            }

            return View(demandeur);
        }

        // GET: Demandeurs/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandeur demandeur = _demandeurService.GetDemandeurByID(id);
            if (demandeur == null)
            {
                return HttpNotFound();
            }
            return View(demandeur);
        }

        // POST: Demandeurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDDEMANDEUR,NOM,POSTNOM,PRENOM,SEXE")] Demandeur demandeur)
        {
            if (ModelState.IsValid)
            {
                _demandeurService.UpdateDemandeur(demandeur);
                _demandeurService.Save();
                return RedirectToAction("Index");
            }
            return View(demandeur);
        }

        // GET: Demandeurs/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demandeur demandeur = _demandeurService.GetDemandeurByID(id);
            if (demandeur == null)
            {
                return HttpNotFound();
            }
            return View(demandeur);
        }

        // POST: Demandeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demandeur demandeur = _demandeurService.GetDemandeurByID(id);
            _demandeurService.DeleteDemandeur(id);
            _demandeurService.Save();
            return RedirectToAction("Index");
        }

    }
}
