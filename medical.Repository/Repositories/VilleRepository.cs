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
    public interface IVilleRepository : IDisposable
    {
        IEnumerable<Ville> GetVille();
        Ville GetVilleByID(int villeID);
        void InsertVille(Ville ville);
        void DeleteVille(int villeID);
        void UpdateVille(Ville ville);
        void Save();

    }
    public class VilleRepository : IVilleRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public VilleRepository()
        {
            _context = new ApplicationDbContext();
        }
        public VilleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ville> GetVille()
        {
            return _context.Villes.Include(v => v.Territoire).ToList();
        }

        public Ville GetVilleByID(int Id)
        {
            return _context.Villes.Find(Id);
        }

        public void InsertVille(Ville ville)
        {
            _context.Villes.Add(ville);
        }

        public void DeleteVille(int villeID)
        {
            Ville ville = _context.Villes.Find(villeID);
            _context.Villes.Remove(ville);
        }

        public void UpdateVille(Ville ville)
        {
            _context.Entry(ville).State = EntityState.Modified;
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
