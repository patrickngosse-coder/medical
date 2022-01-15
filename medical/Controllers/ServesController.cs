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
    public class ServesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IHopitalService _hopitalService;

        public ServesController()
        {
            _serviceService = new ServiceService();
            _hopitalService = new HopitalService();
        }

        public ServesController(IServiceService serviceService, IHopitalService hopitalService)
        {
            _serviceService = serviceService;
            _hopitalService = hopitalService;
        }

        // GET: Serves
        public ActionResult Index()
        {
            var services = from s in _serviceService.GetServe() select s;
            return View(services.ToList());
        }

        public PartialViewResult Index2()
        {
            var services = from s in _serviceService.GetServe() select s;
            return PartialView("_PartialService2", services.ToList());
        }

        // GET: Serves
        public PartialViewResult ServiceIndex()
        {
            var services = from s in _serviceService.GetServe() select s;
            return PartialView("_PartialService", services.ToList());
        }

        //Etat courant
        public PartialViewResult MyServices()
        {
            string userid = User.Identity.GetUserName();
            var data = from service in _serviceService.GetServe()
                       where service.Examens.Select(i => i.Facturations.Select(j => j.Medecin.Email == userid).FirstOrDefault()).FirstOrDefault()
                       select service;

            return PartialView("_PartialMyService", data.ToList());
        }


        //Export
        public ActionResult Reports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/Service.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "ServiceDataSet",
                Value = from s in _serviceService.GetServe() select s
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

        // GET: Serves/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serve serve = _serviceService.GetServeByID(id);
            if (serve == null)
            {
                return HttpNotFound();
            }
            return View(serve);
        }

        // GET: Serves/Create
        public PartialViewResult Create()
        {
            //ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL",  "DESIGNATION");
            return PartialView();
        }

        // POST: Serves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSERVICE,POUCENTAGEXAMEN, POUCENTAGEDEMAND, DESIGNATION")] Serve serve)
        {
            if (ModelState.IsValid)
            {
               _serviceService.InsertServe(serve);
                _serviceService.Save();
                return RedirectToAction("Index");
            }

            //ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE", serve.IDHOPITAL);
            return View(serve);
        }

        // GET: Serves/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serve serve = _serviceService.GetServeByID(id);
            if (serve == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE", serve.IDHOPITAL);
            return View(serve);
        }

        // POST: Serves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSERVICE, POUCENTAGEXAMEN, POUCENTAGEDEMAND,DESIGNATION")] Serve serve)
        {
            if (ModelState.IsValid)
            {
                _serviceService.UpdateServe(serve);
                _serviceService.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.IDHOPITAL = new SelectList(_hopitalService.GetHopital(), "IDHOPITAL", "COORDONNE", serve.IDHOPITAL);
            return View(serve);
        }

        // GET: Serves/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Serve serve = _serviceService.GetServeByID(id);
            if (serve == null)
            {
                return HttpNotFound();
            }
            return View(serve);
        }

        // POST: Serves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Serve serve = _serviceService.GetServeByID(id);
            _serviceService.DeleteServe(id);
            _serviceService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Service
        public PartialViewResult SearchSerice(string searchString)
        {
            var item = from s in _serviceService.GetServe() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.DESIGNATION.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchService", item.ToList());
        }

    }
}
