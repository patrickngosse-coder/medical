using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IConsultationService
    {
        IEnumerable<Consultation> GetConsultation();
        Consultation GetConsultationByID(int consultationID);
        void InsertConsultation(Consultation consultation);
        void DeleteConsultation(int consultationID);
        void UpdateConsultation(Consultation consultation);
        void Save();

    }
    public class ConsultationService : IConsultationService
    {
        private readonly IConsultationRepository _consultationRepo;

        public ConsultationService()
        {
            _consultationRepo = new ConsultationRepository();
        }

        public ConsultationService(IConsultationRepository consultationRepo)
        {
            _consultationRepo = consultationRepo;
        }

        public IEnumerable<Consultation> GetConsultation() => _consultationRepo.GetConsultation();
        public Consultation GetConsultationByID(int consultationID) => _consultationRepo.GetConsultationByID(consultationID);
        public void InsertConsultation(Consultation consultation) => _consultationRepo.InsertConsultation(consultation);
        public void DeleteConsultation(int consultationID) => _consultationRepo.DeleteConsultation(consultationID);
        public void UpdateConsultation(Consultation consultation) => _consultationRepo.UpdateConsultation(consultation);
        public void Save() => _consultationRepo.Save();

    }
}
