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
    public interface IHopitalRepository : IDisposable
    {
        IEnumerable<Hopital> GetHopital();
        Hopital GetHopitalByID(int hopitalID);
        void InsertHopital(Hopital hopital);
        void DeleteHopital(int hopitalID);
        void UpdateHopital(Hopital hopital);
        void Save();

    }
    public class HopitalRepository : IHopitalRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public HopitalRepository()
        {
            _context = new ApplicationDbContext();
        }
        public HopitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Hopital> GetHopital()
        {
            return _context.Hopitals.Include(h => h.Ville).ToList();
        }

        public Hopital GetHopitalByID(int Id)
        {
            return _context.Hopitals.Find(Id);
        }

        public void InsertHopital(Hopital hopital)
        {
            _context.Hopitals.Add(hopital);
        }

        public void DeleteHopital(int hopitalID)
        {
            Hopital hopital = _context.Hopitals.Find(hopitalID);
            _context.Hopitals.Remove(hopital);
        }

        public void UpdateHopital(Hopital hopital)
        {
            _context.Entry(hopital).State = EntityState.Modified;
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
