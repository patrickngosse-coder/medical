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
    public interface IExamenRepository : IDisposable
    {
        IEnumerable<Examen> GetExamen();
        Examen GetExamenByID(int examenID);
        void InsertExamen(Examen examen);
        void DeleteExamen(int examenID);
        void UpdateExamen(Examen examen);
        void Save();

    }
    public class ExamenRepository : IExamenRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public ExamenRepository()
        {
            _context = new ApplicationDbContext();
        }
        public ExamenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Examen> GetExamen()
        {
            return _context.Examens.Include(e => e.Service).ToList();
        }

        public Examen GetExamenByID(int Id)
        {
            return _context.Examens.Find(Id);
        }

        public void InsertExamen(Examen examen)
        {
            _context.Examens.Add(examen);
        }

        public void DeleteExamen(int examenID)
        {
            Examen examen = _context.Examens.Find(examenID);
            _context.Examens.Remove(examen);
        }

        public void UpdateExamen(Examen examen)
        {
            _context.Entry(examen).State = EntityState.Modified;
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
