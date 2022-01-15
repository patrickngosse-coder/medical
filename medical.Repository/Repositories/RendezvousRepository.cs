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
    public interface IRendezvousRepository : IDisposable
    {
        IEnumerable<Rendezvous> GetRendezvous();
        Rendezvous GetRendezvousByID(int rendezvousID);
        void InsertRendezvous(Rendezvous rendezvous);
        void DeleteRendezvous(int rendezvousID);
        void UpdateRendezvous(Rendezvous rendezvous);
        void Save();

    }
    public class RendezvousRepository : IRendezvousRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public RendezvousRepository()
        {
            _context = new ApplicationDbContext();
        }
        public RendezvousRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rendezvous> GetRendezvous()
        {
            return _context.Rendezvouses.ToList();
        }

        public Rendezvous GetRendezvousByID(int Id)
        {
            return _context.Rendezvouses.Find(Id);
        }

        public void InsertRendezvous(Rendezvous rendezvous)
        {
            _context.Rendezvouses.Add(rendezvous);
        }

        public void DeleteRendezvous(int rendezvousID)
        {
            Rendezvous rendezvous = _context.Rendezvouses.Find(rendezvousID);
            _context.Rendezvouses.Remove(rendezvous);
        }

        public void UpdateRendezvous(Rendezvous rendezvous)
        {
            _context.Entry(rendezvous).State = EntityState.Modified;
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
