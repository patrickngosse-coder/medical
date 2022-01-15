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
    public class AgentsController : Controller
    {
        private readonly IAgentService _agentService;
        private readonly IServiceService _serveService;

        public AgentsController()
        {
            _agentService = new AgentService();
            _serveService = new ServiceService();
        }

        public AgentsController(IAgentService agentService, IServiceService serveService)
        {
            _agentService = agentService;
            _serveService = serveService; 
        }

        // GET: Agents
        public ActionResult Index()
        {
            var item = from s in _agentService.GetAgent() select s;
            return View(item.ToList());
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
                Value = from s in _agentService.GetAgent()
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

        // GET: Agents/Details/5
        public ActionResult Details(string id)
        {
            Agent agent = _agentService.GetAgentByID(id);
            return View(agent);
        }

        // GET: Agents/Create
        public ActionResult Create()
        {
            ViewBag.IDSERVICE = new SelectList(_serveService.GetServe(), "IDSERVICE", "DESIGNATION");
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NOM,POSTNOM,PRENOM,SEXE,LIEUNAISSANCE,DATENAISSANCE,ETATCIVIL,MATRICULE,GRADE,IDSERVICE,ADRESSSE")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _agentService.InsertAgent(agent);
                _agentService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDSERVICE = new SelectList(_serveService.GetServe(), "IDSERVICE", "DESIGNATION", agent.IDSERVICE);
            return View(agent);
        }

        // GET: Agents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = _agentService.GetAgentByID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NOM,POSTNOM,PRENOM,SEXE,LIEUNAISSANCE,DATENAISSANCE,ETATCIVIL,MATRICULE,GRADE,IDSERVICE,ADRESSSE")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                _agentService.UpdateAgent(agent);
                _agentService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDSERVICE = new SelectList(_serveService.GetServe(), "IDSERVICE", "DESIGNATION", agent.IDSERVICE);
            return View(agent);
        }

        // GET: Agents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = _agentService.GetAgentByID(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Agent agent = _agentService.GetAgentByID(id);
            _agentService.DeleteAgent(id);
            _agentService.Save();
            return RedirectToAction("Index");
        }

        //SEARCH:Agent
        public PartialViewResult SearchAgent(string searchString)
        {
            var item = from s in _agentService.GetAgent() select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                item = item.Where(s => s.NOM.ToUpper().Contains(searchString.ToUpper()) || s.POSTNOM.ToUpper().Contains(searchString.ToUpper()) || s.PRENOM.ToUpper().Contains(searchString.ToUpper()));
            }

            return PartialView("_PartialSearchAgent", item.ToList());
        }
    }
}
