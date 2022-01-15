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
    public class CompterendusController : Controller
    {
        private readonly ICompterenduService _compterenduService;
        private readonly IActeInfiemierService _acteInfiemierService;
        private readonly IActeMedicalService _acteMedicalService;
        private readonly IPatientService _patientService;
        private readonly IExamenService _examenService;

        public CompterendusController()
        {
            _compterenduService = new CompterenduService();
            _acteInfiemierService = new ActeInfiemierService();
            _acteMedicalService = new ActeMedicalService();
            _patientService = new PatientService();
            _examenService = new ExamenService();
        }

        public CompterendusController(ICompterenduService compterenduService, IActeInfiemierService acteInfiemierService, IActeMedicalService acteMedicalService, IPatientService patientService, IExamenService examenService)
        {
            _compterenduService = compterenduService;
            _acteInfiemierService = acteInfiemierService;
            _acteMedicalService = acteMedicalService;
            _patientService = patientService;
            _examenService = examenService;
        }

        // GET: Compterendus
        public ActionResult Index()
        {
            var compterendus = from s in _compterenduService.GetCompterendu() select s;
            return View(compterendus.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _compterenduService.GetCompterendu();
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

        // GET: Compterendus/Details/5
        public ActionResult Details(int id)
        {
            Compterendu compterendu = _compterenduService.GetCompterenduByID(id);
            return View(compterendu);
        }

        // GET: Compterendus/Create
        public PartialViewResult Create()
        {
            ViewBag.IDACTEINFIRMIER = new SelectList(_acteInfiemierService.GetActeInfiemier(), "IDACTEINFIRMIER", "ACTE");
            ViewBag.IDACTMEDICO = new SelectList(_acteMedicalService.GetActeMedical(), "IDACTMEDICO", "ACTE");
            ViewBag.EXAMENID = new SelectList(_examenService.GetExamen(), "EXAMENID", "CODE");
            return PartialView();
        }

        // POST: Compterendus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCMPTERENDU,RAPPORT,IDACTMEDICO,IDACTEINFIRMIER,EXAMENID,PATHOLOGIE")] Compterendu compterendu)
        {
            if (ModelState.IsValid)
            {
                _compterenduService.InsertCompterendu(compterendu);
                _compterenduService.Save();
                return RedirectToAction("Index");
            }

            ViewBag.IDACTEINFIRMIER = new SelectList(_acteInfiemierService.GetActeInfiemier(), "IDACTEINFIRMIER", "ACTE", compterendu.IDACTEINFIRMIER);
            ViewBag.IDACTMEDICO = new SelectList(_acteMedicalService.GetActeMedical(), "IDACTMEDICO", "ACTE", compterendu.IDACTMEDICO);
            ViewBag.EXAMENID = new SelectList(_examenService.GetExamen(), "EXAMENID", "CODE", compterendu.EXAMENID);
            return View(compterendu);
        }

        // GET: Compterendus/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compterendu compterendu = _compterenduService.GetCompterenduByID(id);
            if (compterendu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDACTEINFIRMIER = new SelectList(_acteInfiemierService.GetActeInfiemier(), "IDACTEINFIRMIER", "ACTE", compterendu.IDACTEINFIRMIER);
            ViewBag.IDACTMEDICO = new SelectList(_acteMedicalService.GetActeMedical(), "IDACTMEDICO", "ACTE", compterendu.IDACTMEDICO);
            ViewBag.EXAMENID = new SelectList(_examenService.GetExamen(), "EXAMENID", "CODE", compterendu.EXAMENID);
            return View(compterendu);
        }

        // POST: Compterendus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCMPTERENDU,RAPPORT,IDACTMEDICO,IDACTEINFIRMIER,EXAMENID,PATHOLOGIE")] Compterendu compterendu)
        {
            if (ModelState.IsValid)
            {
                _compterenduService.UpdateCompterendu(compterendu);
                _compterenduService.Save();
                return RedirectToAction("Index");
            }
            ViewBag.IDACTEINFIRMIER = new SelectList(_acteInfiemierService.GetActeInfiemier(), "IDACTEINFIRMIER", "ACTE", compterendu.IDACTEINFIRMIER);
            ViewBag.IDACTMEDICO = new SelectList(_acteMedicalService.GetActeMedical(), "IDACTMEDICO", "ACTE", compterendu.IDACTMEDICO);
            ViewBag.EXAMENID = new SelectList(_examenService.GetExamen(), "EXAMENID", "CODE", compterendu.EXAMENID);
            return View(compterendu);
        }

        // GET: Compterendus/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compterendu compterendu = _compterenduService.GetCompterenduByID(id);
            if (compterendu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDACTEINFIRMIER = new SelectList(_acteInfiemierService.GetActeInfiemier(), "IDACTEINFIRMIER", "ACTE", compterendu.IDACTEINFIRMIER);
            ViewBag.IDACTMEDICO = new SelectList(_acteMedicalService.GetActeMedical(), "IDACTMEDICO", "ACTE", compterendu.IDACTMEDICO);
            ViewBag.EXAMENID = new SelectList(_examenService.GetExamen(), "EXAMENID", "CODE", compterendu.EXAMENID);
            return View(compterendu);
        }

        // POST: Compterendus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compterendu compterendu = _compterenduService.GetCompterenduByID(id);
            _compterenduService.DeleteCompterendu(id);
            _compterenduService.Save();
            return RedirectToAction("Index");
        }

    }
}
