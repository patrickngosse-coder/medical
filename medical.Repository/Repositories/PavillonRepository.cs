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
    public interface IPavillonRepository : IDisposable
    {
        IEnumerable<Pavillon> GetPavillon();
        Pavillon GetPavillonByID(int pavillonID);
        void InsertPavillon(Pavillon pavillon);
        void DeletePavillon(int pavillonID);
        void UpdatePavillon(Pavillon pavillon);
        void Save();

    }
    public class PavillonRepository : IPavillonRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public PavillonRepository()
        {
            _context = new ApplicationDbContext();
        }
        public PavillonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pavillon> GetPavillon()
        {
            return _context.Pavillons.Include(p => p.Service).ToList();
        }

        public Pavillon GetPavillonByID(int Id)
        {
            return _context.Pavillons.Find(Id);
        }

        public void InsertPavillon(Pavillon pavillon)
        {
            _context.Pavillons.Add(pavillon);
        }

        public void DeletePavillon(int pavillonID)
        {
            Pavillon pavillon = _context.Pavillons.Find(pavillonID);
            _context.Pavillons.Remove(pavillon);
        }

        public void UpdatePavillon(Pavillon pavillon)
        {
            _context.Entry(pavillon).State = EntityState.Modified;
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
