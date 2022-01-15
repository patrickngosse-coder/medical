using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IMedecinService
    {
        IEnumerable<Medecin> GetMedecin();
        Medecin GetMedecinByID(string acteInfiemierID);
        void InsertMedecin(Medecin acteInfiemier);
        void DeleteMedecin(string acteInfiemierID);
        void UpdateMedecin(Medecin acteInfiemier);
        void Save();

    }
    public class MedecinService : IMedecinService
    {
        private readonly IMedecinRepository _acteInfiemierRepo;

        public MedecinService()
        {
            _acteInfiemierRepo = new MedecinRepository();
        }
        public MedecinService(IMedecinRepository acteInfiemierRepo)
        {
            _acteInfiemierRepo = acteInfiemierRepo;
        }

        public IEnumerable<Medecin> GetMedecin() => _acteInfiemierRepo.GetMedecin();
        public Medecin GetMedecinByID(string acteInfiemierID) => _acteInfiemierRepo.GetMedecinByID(acteInfiemierID);
        public void InsertMedecin(Medecin acteInfiemier) => _acteInfiemierRepo.InsertMedecin(acteInfiemier);
        public void DeleteMedecin(string acteInfiemierID) => _acteInfiemierRepo.DeleteMedecin(acteInfiemierID);
        public void UpdateMedecin(Medecin acteInfiemier) => _acteInfiemierRepo.UpdateMedecin(acteInfiemier);
        public void Save() => _acteInfiemierRepo.Save();

    }
}
