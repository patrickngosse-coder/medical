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
    public interface IHospitalisationRepository : IDisposable
    {
        IEnumerable<Hospitalisation> GetHospitalisation();
        Hospitalisation GetHospitalisationByID(int hospitalisationID);
        void InsertHospitalisation(Hospitalisation hospitalisation);
        void DeleteHospitalisation(int hospitalisationID);
        void UpdateHospitalisation(Hospitalisation hospitalisation);
        void Save();

    }
    public class HospitalisationRepository : IHospitalisationRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public HospitalisationRepository()
        {
            _context = new ApplicationDbContext();
        }
        public HospitalisationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Hospitalisation> GetHospitalisation()
        {
            return _context.Hospitalisations.Include(h => h.Pavillon).ToList();
        }

        public Hospitalisation GetHospitalisationByID(int Id)
        {
            return _context.Hospitalisations.Find(Id);
        }

        public void InsertHospitalisation(Hospitalisation hospitalisation)
        {
            _context.Hospitalisations.Add(hospitalisation);
        }

        public void DeleteHospitalisation(int hospitalisationID)
        {
            Hospitalisation hospitalisation = _context.Hospitalisations.Find(hospitalisationID);
            _context.Hospitalisations.Remove(hospitalisation);
        }

        public void UpdateHospitalisation(Hospitalisation hospitalisation)
        {
            _context.Entry(hospitalisation).State = EntityState.Modified;
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
