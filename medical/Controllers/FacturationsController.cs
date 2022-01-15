using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Reporting.WebForms;

namespace medical.Controllers
{
    [Authorize]
    public class FacturationsController : Controller
    {
        private readonly IFacturationService _facturationService;
        private readonly IDemandeurService _demandeurService;
        private readonly IExamenService _examenService;
        private readonly IPatientService _patientService;
        private readonly IMedecinService _medecinService;
        public readonly IServiceService _serviceService;
        public FacturationsController()
        {
            _facturationService = new FacturationService();
            _demandeurService = new DemandeurService();
            _examenService = new ExamenService();
            _patientService = new PatientService();
            _medecinService = new MedecinService();
            _serviceService = new ServiceService();
        }

        public FacturationsController(IFacturationService facturationService, IDemandeurService demandeurService, IExamenService examenService, IPatientService patientService, IMedecinService medecinService, IServiceService serviceService)
        {
            _facturationService = facturationService;
            _demandeurService = demandeurService;
            _examenService = examenService;
            _patientService = patientService;
            _medecinService = medecinService;
            _serviceService = serviceService;
        }

        // GET: Facturations
        public ActionResult Index()
        {
            var facturations = from s in _facturationService.GetFacturation() select s;
            return View(facturations.ToList());
        }

        // GET: Facturations
        public ActionResult FacturationIndex()
        {
            var data = from s in _facturationService.GetFacturation() select s;
            return PartialView("_PartialIndex", data.ToList());
        }

        public ActionResult Recette()
        {
            return View();
        }
        public ActionResult Caisse()
        {
            return View();
        }
        public ActionResult SoldeMedecin()
        {
            return View();
        }
        public PartialViewResult CumuleJournalier()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       group paiement.Examen.Cout 
                       by paiement.DateJour 
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new FacturationJournaliere()
                       {
                           JourS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCumuleRecetteJour", data.ToList());
        }

        public PartialViewResult CumuleHebdo()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       group paiement.Examen.Cout
                       by paiement.DateSemaine 
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new FacturationHebdo()
                       {
                           SemaineS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCumuleRecetteHebdo", data.ToList());
        }

        //Compte Domandeur
        public PartialViewResult EtatDemandeur()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       group (paiement.Examen.Service.PourcentageDem*paiement.Examen.PRIXUNITAIRE)/100
                       by paiement.Demandeur.NomComplet 
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatDemandeur()
                       {
                           DemandeurS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialEtatDemandeur", data.ToList());
        }

        //Compte Examinateur
        public PartialViewResult EtatExaminateur()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       group (paiement.Examen.Service.POUCENTAGEXAMEN * paiement.Examen.PRIXUNITAIRE) / 100
                       by paiement.Medecin.NomComplet 
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatExaminateur()
                       {
                           ExaminateurS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialEtatExaminateur", data.ToList());
        }

        //Compte Principal
        public PartialViewResult EtatPrincipal()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       group (paiement.Examen.PRIXUNITAIRE + paiement.Examen.MATIERE)-(paiement.Examen.Service.PourcentageDem + paiement.Examen.Service.POUCENTAGEXAMEN)
                       by paiement.DateJour into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatPrincipal()
                       {
                           CompteS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialComptePrincipal", data.ToList());
        }

        //Compte Principal
        public PartialViewResult EtatSecondaire()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Examen.PRIXUNITAIRE > paiement.Examen.Cout
                       group (paiement.Examen.Service.PourcentageDem + paiement.Examen.Service.POUCENTAGEXAMEN) - paiement.Examen.PRIXUNITAIRE + paiement.Examen.MATIERE
                       by paiement.DateJour 
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatSecondaire()
                       {
                           CompteS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCompteSecondaire", data.ToList());
        }
        //Etat courant
        public PartialViewResult EtatCourant()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.DateJour == DateTime.Today
                       group (paiement.Examen.PRIXUNITAIRE + paiement.Examen.MATIERE)-(paiement.Examen.Service.PourcentageDem + paiement.Examen.Service.PourcentageExam)
                       by paiement.Examen.DESIGNATION
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatCourant()
                       {
                           CompteS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCourant", data.ToList());
        }

        //Etat courant
        public PartialViewResult EtatPatient()
        {
            string userid = User.Identity.GetUserId();
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Medecin.Email == User.Identity.GetUserName()
                       group (paiement.Examen.Service.POUCENTAGEXAMEN * paiement.Examen.PRIXUNITAIRE) / 100
                       by paiement.Medecin.NomComplet
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatExaminateur()
                       {
                           ExaminateurS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialCourant", data.ToList());
        }

        //Etat courant
        public PartialViewResult Patients()
        {
            string userid = User.Identity.GetUserName();
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Medecin.Email == userid 
                       select paiement;
                      
            return PartialView("_PartialPatient", data.ToList());
        }



        public ActionResult ListPatient()
        {
            string userid = User.Identity.GetUserName();
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Medecin.Email == userid
                       select paiement;
                      
            return View(data.ToList());
        }

        public PartialViewResult MyHonoraires()
        {
            string userid = User.Identity.GetUserName();
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Medecin.Email == userid
                       group (paiement.Examen.Service.POUCENTAGEXAMEN * paiement.Examen.PRIXUNITAIRE) / 100
                       by paiement.Medecin.NomComplet
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatExaminateur()
                       {
                           ExaminateurS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return PartialView("_PartialEtatExam", data.ToList());
        }

        public ActionResult ListMyHonoraires()
        {
            string userid = User.Identity.GetUserName();
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Medecin.Email == userid
                       group (paiement.Examen.Service.POUCENTAGEXAMEN * paiement.Examen.PRIXUNITAIRE) / 100
                       by paiement.DateSemaine
                       into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new MyHonoraire()
                       {
                           JourS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return View(data.ToList());
        }

        //Export
        public ActionResult Reports(string ReportType)
        {
            string userid = User.Identity.GetUserName();

            LocalReport localReport = new LocalReport
            {
                ReportPath = Server.MapPath("~/Reports/Facturation.rdlc")
            };

            ReportDataSource reportDataSource = new ReportDataSource
            {
                Name = "FacturationDataSet",
                Value = from s in _facturationService.GetFacturation() select s
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

        //Compte Principal
        public ActionResult Graphique()
        {
            var data = from paiement in _facturationService.GetFacturation()
                       where paiement.Examen.PRIXUNITAIRE == paiement.Examen.Cout
                       group (paiement.Examen.Service.PourcentageDem + paiement.Examen.Service.POUCENTAGEXAMEN) - paiement.Examen.PRIXUNITAIRE + paiement.Examen.MATIERE
                       by paiement.DateJour into paiementGroup
                       orderby paiementGroup.Sum() descending
                       select new EtatPrincipal()
                       {
                           CompteS = paiementGroup.Key,
                           PaiementSomme = paiementGroup.Sum()
                       };

            return View(data.ToList());
        }


        //Etat courant
        public ActionResult SemainePatient()
        {
            string userid = User.Identity.GetUserId();
            var data = from paiement in _facturationService.GetFacturation()
                       group paiement.Patient
                       by paiement.DateSemaine
                       into paiementGroup
                       orderby paiementGroup.Count() descending
                       select new PatientSemaine()
                       {
                           JourS = paiementGroup.Key,
                           NbrePatient = paiementGroup.Distinct().Count()
                       };

            return View(data.ToList());
        }


        // GET: Facturations/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturation facturation = _facturationService.GetFacturationByID(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            return View(facturation);
        }

        // GET: Facturations/Create
        public PartialViewResult Create()
        {
            ViewBag.IDDEMANDEUR = new SelectList(_demandeurService.GetDemandeur(), "IDDEMANDEUR", "NomComplet");
            ViewBag.IDEXAMEN = new SelectList(_examenService.GetExamen(), "IDEXAMEN", "Souche");
            //ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet");
            ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet");
            return PartialView();
        }

        // POST: Facturations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDFACTURATION,REFERENCE,DATEFACTURE,DateJour,DateSemaine,DateAnne,CATEGORIE,JOUR,IDEXAMEN,Id,IDDEMANDEUR,QUANTITE,Montant")] Facturation facturation)
        {
            if (ModelState.IsValid)
            {
                _facturationService.InsertFacturation(facturation);
                _facturationService.Save();
                return RedirectToAction("Index");
            }

           // ViewBag.IDDEMANDEUR = new SelectList(_demandeurService.GetDemandeur(), "IDDEMANDEUR", "NomComplet", facturation.IDDEMANDEUR);
           // ViewBag.IDEXAMEN = new SelectList(_examenService.GetExamen(), "IDEXAMEN", "Souche", facturation.IDEXAMEN);
           //// ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", facturation.IDPATIENT);
           // ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", facturation.Id);
            return View(facturation);
        }

        // GET: Facturations/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturation facturation = _facturationService.GetFacturationByID(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDDEMANDEUR = new SelectList(_demandeurService.GetDemandeur(), "IDDEMANDEUR", "NomComplet", facturation.IDDEMANDEUR);
            //ViewBag.IDEXAMEN = new SelectList(_examenService.GetExamen(), "IDEXAMEN", "Souche", facturation.IDEXAMEN);
            ////ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", facturation.IDPATIENT);
            //ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", facturation.Id);
            return View(facturation);
        }

        // POST: Facturations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDFACTURATION,REFERENCE,DATEFACTURE,DateJour,DateSemaine,DateAnne,IDEXAMEN,CATEGORIE,JOUR,Id,IDDEMANDEUR,QUANTITE,Montant")] Facturation facturation)
        {
            if (ModelState.IsValid)
            {
                _facturationService.UpdateFacturation(facturation);
                _facturationService.Save();
                return RedirectToAction("Index");
            }
            //ViewBag.IDDEMANDEUR = new SelectList(_demandeurService.GetDemandeur(), "IDDEMANDEUR", "NomComplet", facturation.IDDEMANDEUR);
            //ViewBag.IDEXAMEN = new SelectList(_examenService.GetExamen(), "IDEXAMEN", "Souche", facturation.IDEXAMEN);
            ////ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", facturation.IDPATIENT);
            //ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", facturation.Id);
            return View(facturation);
        }

        // GET: Facturations/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturation facturation = _facturationService.GetFacturationByID(id);
            if (facturation == null)
            {
                return HttpNotFound();
            }
            //ViewBag.IDDEMANDEUR = new SelectList(_demandeurService.GetDemandeur(), "IDDEMANDEUR", "NomComplet", facturation.IDDEMANDEUR);
            //ViewBag.IDEXAMEN = new SelectList(_examenService.GetExamen(), "IDEXAMEN", "Souche", facturation.IDEXAMEN);
            ////ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet", facturation.IDPATIENT);
            //ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet", facturation.Id);
            return View(facturation);
        }

        // POST: Facturations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturation facturation = _facturationService.GetFacturationByID(id);
            _facturationService.DeleteFacturation(id);
            _facturationService.Save();
            return RedirectToAction("Index");
        }


        public ActionResult DetailPatient(int? id, int? id2)
        {
            string userid = User.Identity.GetUserName();

            var viewModel = new IndexPatient();
            viewModel.Services = _serviceService.GetServe();

            if (id != null)
            {
                ViewBag.IDSERVICE = id.Value;
                viewModel.Examens = viewModel.Services.Where(i => i.IDSERVICE == id.Value).Single().Examens;

                if (id2 != null)
                {
                    ViewBag.IDEXAMEN = id.Value;
                    viewModel.Facturations = viewModel.Examens.Where(i => i.IDEXAMEN == id.Value).Single().Facturations;
                }
            }

            return View(viewModel);

        }

        public ActionResult MyPatients(System.Guid id)
        {
            string userid = User.Identity.GetUserName();
          
            var viewModel = new IndexPatient();

            viewModel.Facturations = from paiement in _facturationService.GetFacturation()
                                     where paiement.Medecin.Email == userid
                                     select paiement; 
            if (id != null)
            {
                ViewBag.IDFACTURATION = id;
                viewModel.Examens = viewModel.Facturations.Where(i => i.IDFACTURATION == id).Select(j=>j.Examen);

            }
            return View(viewModel);

        }
    }
}
