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
    public interface IDemandeurRepository : IDisposable
    {
        IEnumerable<Demandeur> GetDemandeur();
        Demandeur GetDemandeurByID(int demandeurID);
        void InsertDemandeur(Demandeur demandeur);
        void DeleteDemandeur(int demandeurID);
        void UpdateDemandeur(Demandeur demandeur);
        void Save();

    }
    public class DemandeurRepository : IDemandeurRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public DemandeurRepository()
        {
            _context = new ApplicationDbContext();
        }
        public DemandeurRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Demandeur> GetDemandeur()
        {
            return _context.Demandeurs.ToList();
        }

        public Demandeur GetDemandeurByID(int Id)
        {
            return _context.Demandeurs.Find(Id);
        }

        public void InsertDemandeur(Demandeur demandeur)
        {
            _context.Demandeurs.Add(demandeur);
        }

        public void DeleteDemandeur(int demandeurID)
        {
            Demandeur demandeur = _context.Demandeurs.Find(demandeurID);
            _context.Demandeurs.Remove(demandeur);
        }

        public void UpdateDemandeur(Demandeur demandeur)
        {
            _context.Entry(demandeur).State = EntityState.Modified;
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
