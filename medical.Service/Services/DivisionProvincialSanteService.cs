using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IDivisionProvincialSanteService
    {
        IEnumerable<DivisionProvincialSante> GetDivisionProvincialSante();
        DivisionProvincialSante GetDivisionProvincialSanteByID(int divisionProvincialSanteID);
        void InsertDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante);
        void DeleteDivisionProvincialSante(int divisionProvincialSanteID);
        void UpdateDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante);
        void Save();

    }
    public class DivisionProvincialSanteService : IDivisionProvincialSanteService
    {
        private readonly IDivisionProvincialSanteRepository _divisionProvincialSanteRepo;

        public DivisionProvincialSanteService()
        {
            _divisionProvincialSanteRepo = new DivisionProvincialSanteRepository();
        }

        public DivisionProvincialSanteService(IDivisionProvincialSanteRepository divisionProvincialSanteRepo)
        {
            _divisionProvincialSanteRepo = divisionProvincialSanteRepo;
        }

        public IEnumerable<DivisionProvincialSante> GetDivisionProvincialSante() => _divisionProvincialSanteRepo.GetDivisionProvincialSante();
        public DivisionProvincialSante GetDivisionProvincialSanteByID(int divisionProvincialSanteID) => _divisionProvincialSanteRepo.GetDivisionProvincialSanteByID(divisionProvincialSanteID);
        public void InsertDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante) => _divisionProvincialSanteRepo.InsertDivisionProvincialSante(divisionProvincialSante);
        public void DeleteDivisionProvincialSante(int divisionProvincialSanteID) => _divisionProvincialSanteRepo.DeleteDivisionProvincialSante(divisionProvincialSanteID);
        public void UpdateDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante) => _divisionProvincialSanteRepo.UpdateDivisionProvincialSante(divisionProvincialSante);
        public void Save() => _divisionProvincialSanteRepo.Save();

    }
}
