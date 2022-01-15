using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface ITerritoireService
    {
        IEnumerable<Territoire> GetTerritoire();
        Territoire GetTerritoireByID(int territoireID);
        void InsertTerritoire(Territoire territoire);
        void DeleteTerritoire(int territoireID);
        void UpdateTerritoire(Territoire territoire);
        void Save();

    }
    public class TerritoireService : ITerritoireService
    {
        private readonly ITerritoireRepository _territoireRepo;
        public TerritoireService()
        {
            _territoireRepo = new TerritoireRepository();
        }
        public TerritoireService(ITerritoireRepository territoireRepo)
        {
            _territoireRepo = territoireRepo;
        }

        public IEnumerable<Territoire> GetTerritoire() => _territoireRepo.GetTerritoire();
        public Territoire GetTerritoireByID(int territoireID) => _territoireRepo.GetTerritoireByID(territoireID);
        public void InsertTerritoire(Territoire territoire) => _territoireRepo.InsertTerritoire(territoire);
        public void DeleteTerritoire(int territoireID) => _territoireRepo.DeleteTerritoire(territoireID);
        public void UpdateTerritoire(Territoire territoire) => _territoireRepo.UpdateTerritoire(territoire);
        public void Save() => _territoireRepo.Save();

    }
}
