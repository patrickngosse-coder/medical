using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface ICentreSanteService
    {
        IEnumerable<CentreSante> GetCentreSante();
        CentreSante GetCentreSanteByID(int centreSanteID);
        void InsertCentreSante(CentreSante centreSante);
        void DeleteCentreSante(int centreSanteID);
        void UpdateCentreSante(CentreSante centreSante);
        void Save();

    }
    public class CentreSanteService : ICentreSanteService
    {
        private readonly ICentreSanteRepository _centreSanteRepo;

        public CentreSanteService()
        {
            _centreSanteRepo = new CentreSanteRepository();
        }
        public CentreSanteService(ICentreSanteRepository centreSanteRepo)
        {
            _centreSanteRepo = centreSanteRepo;
        }

        public IEnumerable<CentreSante> GetCentreSante() => _centreSanteRepo.GetCentreSante();
        public CentreSante GetCentreSanteByID(int centreSanteID) => _centreSanteRepo.GetCentreSanteByID(centreSanteID);
        public void InsertCentreSante(CentreSante centreSante) => _centreSanteRepo.InsertCentreSante(centreSante);
        public void DeleteCentreSante(int centreSanteID) => _centreSanteRepo.DeleteCentreSante(centreSanteID);
        public void UpdateCentreSante(CentreSante centreSante) => _centreSanteRepo.UpdateCentreSante(centreSante);
        public void Save() => _centreSanteRepo.Save();

    }
}
