using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IExamenService
    {
        IEnumerable<Examen> GetExamen();
        Examen GetExamenByID(int examenID);
        void InsertExamen(Examen examen);
        void DeleteExamen(int examenID);
        void UpdateExamen(Examen examen);
        void Save();

    }
    public class ExamenService : IExamenService
    {
        private readonly IExamenRepository _examenRepo;

        public ExamenService()
        {
            _examenRepo = new ExamenRepository();
        }

        public ExamenService(IExamenRepository examenRepo)
        {
            _examenRepo = examenRepo;
        }

        public IEnumerable<Examen> GetExamen() => _examenRepo.GetExamen();
        public Examen GetExamenByID(int examenID) => _examenRepo.GetExamenByID(examenID);
        public void InsertExamen(Examen examen) => _examenRepo.InsertExamen(examen);
        public void DeleteExamen(int examenID) => _examenRepo.DeleteExamen(examenID);
        public void UpdateExamen(Examen examen) => _examenRepo.UpdateExamen(examen);
        public void Save() => _examenRepo.Save();

    }
}
