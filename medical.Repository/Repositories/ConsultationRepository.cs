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
    public interface IConsultationRepository : IDisposable
    {
        IEnumerable<Consultation> GetConsultation();
        Consultation GetConsultationByID(int consultationID);
        void InsertConsultation(Consultation consultation);
        void DeleteConsultation(int consultationID);
        void UpdateConsultation(Consultation consultation);
        void Save();

    }
    public class ConsultationRepository : IConsultationRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public ConsultationRepository()
        {
            _context = new ApplicationDbContext();
        }
        public ConsultationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Consultation> GetConsultation()
        {
            return _context.Consultations.ToList();
        }

        public Consultation GetConsultationByID(int Id)
        {
            return _context.Consultations.Find(Id);
        }

        public void InsertConsultation(Consultation consultation)
        {
            _context.Consultations.Add(consultation);
        }

        public void DeleteConsultation(int consultationID)
        {
            Consultation consultation = _context.Consultations.Find(consultationID);
            _context.Consultations.Remove(consultation);
        }

        public void UpdateConsultation(Consultation consultation)
        {
            _context.Entry(consultation).State = EntityState.Modified;
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
