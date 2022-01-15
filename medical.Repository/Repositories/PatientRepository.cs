using medical.Data;
using medical.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Repository.Repositories
{
    public interface IPatientRepository : IDisposable
    {
        IEnumerable<Patient> GetPatient();
        Patient GetPatientByID(int patientID);
        void InsertPatient(Patient patient);
        void DeletePatient(int patientID);
        void UpdatePatient(Patient patient);
        void Save();

    }
    public class PatientRepository : IPatientRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public PatientRepository()
        {
            _context = new ApplicationDbContext();
        }
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetPatient()
        {
            return _context.Patients.ToList();
        }

        public Patient GetPatientByID(int Id)
        {
            return _context.Patients.Find(Id);
        }

        public void InsertPatient(Patient patient)
        {
            _context.Patients.Add(patient);
        }

        public void DeletePatient(int patientID)
        {
            Patient patient = _context.Patients.Find(patientID);
            _context.Patients.Remove(patient);
        }

        public void UpdatePatient(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
