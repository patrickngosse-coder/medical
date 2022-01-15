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
    public interface IMedecinRepository : IDisposable
    {
        IEnumerable<Medecin> GetMedecin();
        Medecin GetMedecinByID(string medecinID);
        void InsertMedecin(Medecin medecin);
        void DeleteMedecin(string medecinID);
        void UpdateMedecin(Medecin medecin);
        void Save();

    }
    public class MedecinRepository : IMedecinRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public MedecinRepository()
        {
            _context = new ApplicationDbContext();
        }
        public MedecinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Medecin> GetMedecin()
        {
            return _context.Medecins.Include(m => m.Service).ToList();
        }

        public Medecin GetMedecinByID(string Id)
        {
            return _context.Medecins.Find(Id);
        }

        public void InsertMedecin(Medecin medecin)
        {
            _context.Medecins.Add(medecin);
        }

        public void DeleteMedecin(string medecinID)
        {
            Medecin medecin = _context.Medecins.Find(medecinID);
            _context.Medecins.Remove(medecin);
        }

        public void UpdateMedecin(Medecin medecin)
        {
            _context.Entry(medecin).State = EntityState.Modified;
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
