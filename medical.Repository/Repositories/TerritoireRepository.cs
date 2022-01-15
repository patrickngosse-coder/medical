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
    public interface ITerritoireRepository : IDisposable
    {
        IEnumerable<Territoire> GetTerritoire();
        Territoire GetTerritoireByID(int territoireID);
        void InsertTerritoire(Territoire territoire);
        void DeleteTerritoire(int territoireID);
        void UpdateTerritoire(Territoire territoire);
        void Save();

    }
    public class TerritoireRepository : ITerritoireRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public TerritoireRepository()
        {
            _context = new ApplicationDbContext();
        }
        public TerritoireRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Territoire> GetTerritoire()
        {
            return _context.Territoires.Include(t => t.Province).ToList();
        }

        public Territoire GetTerritoireByID(int Id)
        {
            return _context.Territoires.Find(Id);
        }

        public void InsertTerritoire(Territoire territoire)
        {
            _context.Territoires.Add(territoire);
        }

        public void DeleteTerritoire(int territoireID)
        {
            Territoire territoire = _context.Territoires.Find(territoireID);
            _context.Territoires.Remove(territoire);
        }

        public void UpdateTerritoire(Territoire territoire)
        {
            _context.Entry(territoire).State = EntityState.Modified;
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
