using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IHospitalisationService
    {
        IEnumerable<Hospitalisation> GetHospitalisation();
        Hospitalisation GetHospitalisationByID(int hospitalisationID);
        void InsertHospitalisation(Hospitalisation hospitalisation);
        void DeleteHospitalisation(int hospitalisationID);
        void UpdateHospitalisation(Hospitalisation hospitalisation);
        void Save();

    }
    public class HospitalisationService : IHospitalisationService
    {
        private readonly IHospitalisationRepository _hospitalisationRepo;

        public HospitalisationService()
        {
            _hospitalisationRepo = new HospitalisationRepository();
        }

        public HospitalisationService(IHospitalisationRepository hospitalisationRepo)
        {
            _hospitalisationRepo = hospitalisationRepo;
        }

        public IEnumerable<Hospitalisation> GetHospitalisation() => _hospitalisationRepo.GetHospitalisation();
        public Hospitalisation GetHospitalisationByID(int hospitalisationID) => _hospitalisationRepo.GetHospitalisationByID(hospitalisationID);
        public void InsertHospitalisation(Hospitalisation hospitalisation) => _hospitalisationRepo.InsertHospitalisation(hospitalisation);
        public void DeleteHospitalisation(int hospitalisationID) => _hospitalisationRepo.DeleteHospitalisation(hospitalisationID);
        public void UpdateHospitalisation(Hospitalisation hospitalisation) => _hospitalisationRepo.UpdateHospitalisation(hospitalisation);
        public void Save() => _hospitalisationRepo.Save();

    }
}
