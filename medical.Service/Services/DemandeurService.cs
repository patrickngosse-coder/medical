using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IDemandeurService
    {
        IEnumerable<Demandeur> GetDemandeur();
        Demandeur GetDemandeurByID(int demandeurID);
        void InsertDemandeur(Demandeur demandeur);
        void DeleteDemandeur(int demandeurID);
        void UpdateDemandeur(Demandeur demandeur);
        void Save();

    }
    public class DemandeurService : IDemandeurService
    {
        private readonly IDemandeurRepository _demandeurRepo;

        public DemandeurService()
        {
            _demandeurRepo = new DemandeurRepository();
        }

        public DemandeurService(IDemandeurRepository demandeurRepo)
        {
            _demandeurRepo = demandeurRepo;
        }

        public IEnumerable<Demandeur> GetDemandeur() => _demandeurRepo.GetDemandeur();
        public Demandeur GetDemandeurByID(int demandeurID) => _demandeurRepo.GetDemandeurByID(demandeurID);
        public void InsertDemandeur(Demandeur demandeur) => _demandeurRepo.InsertDemandeur(demandeur);
        public void DeleteDemandeur(int demandeurID) => _demandeurRepo.DeleteDemandeur(demandeurID);
        public void UpdateDemandeur(Demandeur demandeur) => _demandeurRepo.UpdateDemandeur(demandeur);
        public void Save() => _demandeurRepo.Save();

    }
}
