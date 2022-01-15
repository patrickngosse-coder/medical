using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetPatient();
        Patient GetPatientByID(int patientID);
        void InsertPatient(Patient patient);
        void DeletePatient(int patientID);
        void UpdatePatient(Patient patient);
        void Save();

    }
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepo;

        public PatientService()
        {
            _patientRepo = new PatientRepository();
        }

        public PatientService(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public IEnumerable<Patient> GetPatient() => _patientRepo.GetPatient();
        public Patient GetPatientByID(int patientID) => _patientRepo.GetPatientByID(patientID);
        public void InsertPatient(Patient patient) => _patientRepo.InsertPatient(patient);
        public void DeletePatient(int patientID) => _patientRepo.DeletePatient(patientID);
        public void UpdatePatient(Patient patient) => _patientRepo.UpdatePatient(patient);
        public void Save() => _patientRepo.Save();

    }
}
