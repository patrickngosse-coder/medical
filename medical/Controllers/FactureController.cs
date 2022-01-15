using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using medical.Data;
using medical.Model.Models;
using medical.Service.Services;

namespace medical.Controllers
{
    [Authorize]
    public class FactureController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private readonly IFacturationService _facturationService;
        private readonly IDemandeurService _demandeurService;
        private readonly IExamenService _examenService;
        private readonly IPatientService _patientService;
        private readonly IMedecinService _medecinService;
        public readonly IServiceService _serviceService;
        public FactureController()
        {
            _facturationService = new FacturationService();
            _demandeurService = new DemandeurService();
            _examenService = new ExamenService();
            _patientService = new PatientService();
            _medecinService = new MedecinService();
            _serviceService = new ServiceService();
        }

        public FactureController(IFacturationService facturationService, IDemandeurService demandeurService, IExamenService examenService, IPatientService patientService, IMedecinService medecinService, IServiceService serviceService)
        {
            _facturationService = facturationService;
            _demandeurService = demandeurService;
            _examenService = examenService;
            _patientService = patientService;
            _medecinService = medecinService;
            _serviceService = serviceService;
        }

        // GET: Facture
        public ActionResult Index()
        {
            var patients = from s in _patientService.GetPatient() select s;
            return View(patients.ToList());
        }

        // GET: Facture/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Facturation facturation = db.Facturations.Find(id);
        //    if (facturation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(facturation);
        //}

        // GET: Facture/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IDDEMANDEUR = new SelectList(_demandeurService.GetDemandeur(), "IDDEMANDEUR", "NomComplet");
        //    ViewBag.IDEXAMEN = new SelectList(_examenService.GetExamen(), "IDEXAMEN", "Souche");
        //    //ViewBag.IDPATIENT = new SelectList(_patientService.GetPatient(), "IDPATIENT", "NomComplet");
        //    ViewBag.Id = new SelectList(_medecinService.GetMedecin(), "Id", "NomComplet");
        //    return View();
        //}

        //// POST: Facture/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IDFACTURATION,REFERENCE,DATEFACTURE,DateJour,DateSemaine,DateAnne,NOMPATIENT,AGE,SEXE,CATEGORIE,JOUR,IDEXAMEN,Id,IDDEMANDEUR")] Facturation facturation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Facturations.Add(facturation);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IDDEMANDEUR = new SelectList(db.Demandeurs, "IDDEMANDEUR", "NOM", facturation.IDDEMANDEUR);
        //    ViewBag.IDEXAMEN = new SelectList(db.Examens, "IDEXAMEN", "CODE", facturation.IDEXAMEN);
        //    ViewBag.Id = new SelectList(db.Users, "Id", "NOM", facturation.Id);
        //    return View(facturation);
        //}



        public ActionResult SaveFacture(string nom, string postnom, string prenom, string sexe, string adresse,  string telephone, Facturation[] facture)
        {
            string result = "Erreur! Facture n'est pas complet!";
            if (nom != null && postnom != null && prenom != null && adresse != null && telephone != null && facture != null)
            {
                var patientId = Guid.NewGuid();
                Patient model = new Patient();
                model.IDPATIENT = patientId;
                model.NOM = nom;
                model.POSTNOM = postnom;
                model.PRENOM = prenom;
                model.SEXE = sexe;
                model.TEL = telephone;
                model.ADRESSSE = adresse;
                model.DateEnregistrement = DateTime.Now;
                _patientService.InsertPatient(model);
                
                foreach (var item in facture)
                {
                    var orderId = Guid.NewGuid();
                    Facturation f = new Facturation();
                    f.IDFACTURATION = orderId;
                    f.SERVICE = item.SERVICE;
                    f.EXAMEN = item.EXAMEN;
                    f.PRIX = item.PRIX;
                    f.EXAMINATEUR = item.EXAMINATEUR;
                    f.DEMANDEUR = item.DEMANDEUR;
                    //_facturationService.InsertFacturation(f);
                    
                }
                //_patientService.Save();
                //_facturationService.Save();
                db.SaveChanges();
                result = "Reussit! La facture est complète!";

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

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

    }
}
