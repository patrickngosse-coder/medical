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
using medical.Model.ViewModel;
using medical.Service.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;

namespace medical.Controllers
{
    [Authorize]
    public class ExamenController : Controller
    {
        private readonly IExamenService _examenService;
        private readonly IServiceService _serviceService;

        public ExamenController()
        {
            _examenService = new ExamenService();
            _serviceService = new ServiceService();
        }

        public ExamenController(IExamenService examenService, IServiceService serviceService)
        {
            _examenService = examenService;
            _serviceService = serviceService;
        }

        // GET: Examen
        public ActionResult Index()
        {
            var examens = from s in _examenService.GetExamen() select s;
            return View(examens.ToList());
        }

        public ActionResult MyReports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/Examen.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "DataSet1",
                Value = from examen in _examenService.GetExamen()
                        where examen.Facturations.Select(j => j.Medecin.NomComplet == userid).FirstOrDefault()
                        select examen
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



        public ActionResult Reports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/Examen.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "DataSet1",
                Value = from s in _examenService.GetExamen() select s
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
            Response.AddHeader("content-disposition", "attachment;filename=Marklist."+fileNameExtension);
            return File(renderByte, fileNameExtension);
        }



        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _examenService.GetExamen();
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
        public PartialViewResult ExamenIndex()
        {
            var data = from s in _examenService.GetExamen() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        //Etat courant
        public PartialViewResult MyExamens()
        {
            string userid = User.Identity.GetUserName();
            var data = from examen in _examenService.GetExamen()
                       where examen.Facturations.Select(j => j.Medecin.Email == userid).FirstOrDefault()
                       select examen;

            return PartialView("_PartialMyExamen", data.ToList());
        }

        public ActionResult ListMyExamens()
        {
            string userid = User.Identity.GetUserName();
            var data = from examen in _examenService.GetExamen()
                       where examen.Facturations.Select(j => j.Medecin.Email == userid).FirstOrDefault()
                       select examen;

            return View(data.ToList());
        }

        // GET: Examen/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examen examen = _examenService.GetExamenByID(id);
            if (examen == null)
            {
                return HttpNotFound();
            }
            return View(examen);
        }

        // GET: Examen/Create
        public PartialViewResult Create()
        {
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION");
            return PartialView();
        }

        // POST: Examen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDEXAMEN,CODE,IDSERVICE,DESIGNATION,PRIXUNITAIRE,MATIERE")] Examen examen)
        {
            if (ModelState.IsValid)
            {
                _examenService.InsertExamen(examen);
                _examenService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", examen.IDSERVICE);
            return View(examen);
        }

        // GET: Examen/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examen examen = _examenService.GetExamenByID(id);
            if (examen == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", examen.IDSERVICE);
            return View(examen);
        }

        // POST: Examen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDEXAMEN,CODE,IDSERVICE,DESIGNATION,PRIXUNITAIRE,MATIERE")] Examen examen)
        {
            if (ModelState.IsValid)
            {
                _examenService.UpdateExamen(examen);
                _examenService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDSERVICE = new SelectList(_serviceService.GetServe(), "IDSERVICE", "DESIGNATION", examen.IDSERVICE);
            return View(examen);
        }

        // GET: Examen/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Examen examen = _examenService.GetExamenByID(id);
            if (examen == null)
            {
                return HttpNotFound();
            }
            return View(examen);
        }

        // POST: Examen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Examen examen = _examenService.GetExamenByID(id);
            _examenService.DeleteExamen(id);
            _examenService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Examen
        public PartialViewResult SearchExamen(string searchString)
        {
            var item = from s in _examenService.GetExamen() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.DESIGNATION.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchExamen", item.ToList());
        }
    }
}
