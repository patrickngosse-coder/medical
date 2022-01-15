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
    public interface ICentreSanteRepository : IDisposable
    {
        IEnumerable<CentreSante> GetCentreSante();
        CentreSante GetCentreSanteByID(int centreSanteID);
        void InsertCentreSante(CentreSante centreSante);
        void DeleteCentreSante(int centreSanteID);
        void UpdateCentreSante(CentreSante centreSante);
        void Save();

    }
    public class CentreSanteRepository : ICentreSanteRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public CentreSanteRepository()
        {
            _context = new ApplicationDbContext();
        }

        public CentreSanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CentreSante> GetCentreSante()
        {
            return _context.CentreSantes.Include(c => c.ZoneSante).ToList();
        }

        public CentreSante GetCentreSanteByID(int Id)
        {
            return _context.CentreSantes.Find(Id);
        }

        public void InsertCentreSante(CentreSante centreSante)
        {
            _context.CentreSantes.Add(centreSante);
        }

        public void DeleteCentreSante(int centreSanteID)
        {
            CentreSante centreSante = _context.CentreSantes.Find(centreSanteID);
            _context.CentreSantes.Remove(centreSante);
        }

        public void UpdateCentreSante(CentreSante centreSante)
        {
            _context.Entry(centreSante).State = EntityState.Modified;
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
