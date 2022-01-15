using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IActeMedicalService
    {
        IEnumerable<ActeMedical> GetActeMedical();
        ActeMedical GetActeMedicalByID(int acteMedicalID);
        void InsertActeMedical(ActeMedical acteMedical);
        void DeleteActeMedical(int acteMedicalID);
        void UpdateActeMedical(ActeMedical acteMedical);
        void Save();

    }
    public class ActeMedicalService : IActeMedicalService
    {
        private readonly IActeMedicalRepository _acteMedicalRepo;

        public ActeMedicalService()
        {
            _acteMedicalRepo = new ActeMedicalRepository();
        }

        public ActeMedicalService(IActeMedicalRepository acteMedicalRepo)
        {
            _acteMedicalRepo = acteMedicalRepo;
        }

        public IEnumerable<ActeMedical> GetActeMedical() => _acteMedicalRepo.GetActeMedical();
        public ActeMedical GetActeMedicalByID(int acteMedicalID) => _acteMedicalRepo.GetActeMedicalByID(acteMedicalID);
        public void InsertActeMedical(ActeMedical acteMedical) => _acteMedicalRepo.InsertActeMedical(acteMedical);
        public void DeleteActeMedical(int acteMedicalID) => _acteMedicalRepo.DeleteActeMedical(acteMedicalID);
        public void UpdateActeMedical(ActeMedical acteMedical) => _acteMedicalRepo.UpdateActeMedical(acteMedical);
        public void Save() => _acteMedicalRepo.Save();

    }
}
