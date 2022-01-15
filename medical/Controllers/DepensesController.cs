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

namespace medical.Controllers
{
    public class DepensesController : Controller
    {
        private readonly IDepenseService _depenseService;

        public DepensesController()
        {
            _depenseService = new DepenseService();
        }

        public DepensesController(IDepenseService depenseService)
        {
            _depenseService = depenseService;
        }

        // GET: Depenses
        public ActionResult Index()
        {
            var item = from s in _depenseService.GetDepense() select s;
            return View(item.ToList());
        }

        //Export
        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv.DataSource = _depenseService.GetDepense();
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

        // GET: Facturations
        public ActionResult DepenseIndex()
        {
            var data = from s in _depenseService.GetDepense() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        public ActionResult Depense()
        {
            return View();
        }
        //Dépense Journalière
        public PartialViewResult CumuleJournalier()
        {
            var data = from paiement in _depenseService.GetDepense()
                       group paiement.Montant by paiement.DateDay into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new DepenseJournalier()
                       {
                           JourS = paiementGroup.Key,
                           DepenseSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCumuleDepênseJour", data.ToList());
        }

        //Dépense Semaine
        public PartialViewResult CumuleHebdo()
        {
            var data = from paiement in _depenseService.GetDepense()
                       group paiement.Montant by paiement.DateWeek into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new DepenseHebdo()
                       {
                           SemaineS = paiementGroup.Key,
                           DepenseSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCumuleDepênseSemaine", data.ToList());
        }

        // GET: Depenses/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depense depense = _depenseService.GetDepenseByID(id);
            if (depense == null)
            {
                return HttpNotFound();
            }
            return View(depense);
        }

        // GET: Depenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Depenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepenseID,DateEdition,Destinateur,Motif,Montant,DateDay,DateWeek,DateYear")] Depense depense)
        {
            if (ModelState.IsValid)
            {
               _depenseService.InsertDepense(depense);
                _depenseService.Save();
                return RedirectToAction("Index");
            }

            return View(depense);
        }

        // GET: Depenses/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depense depense = _depenseService.GetDepenseByID(id);
            if (depense == null)
            {
                return HttpNotFound();
            }
            return View(depense);
        }

        // POST: Depenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepenseID,DateEdition,Destinateur,Motif,Montant,DateDay,DateWeek,DateYear")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                _depenseService.UpdateDepense(depense);
                _depenseService.Save();
                return RedirectToAction("Index");
            }
            return View(depense);
        }

        // GET: Depenses/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depense depense = _depenseService.GetDepenseByID(id);
            if (depense == null)
            {
                return HttpNotFound();
            }
            return View(depense);
        }

        // POST: Depenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Depense depense = _depenseService.GetDepenseByID(id);
            _depenseService.DeleteDepense(id);
            _depenseService.Save();
            return RedirectToAction("Index");
        }

    }
}
