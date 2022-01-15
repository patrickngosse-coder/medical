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
    public interface IMaladieRepository : IDisposable
    {
        IEnumerable<Maladie> GetMaladie();
        Maladie GetMaladieByID(int maladieID);
        void InsertMaladie(Maladie maladie);
        void DeleteMaladie(int maladieID);
        void UpdateMaladie(Maladie maladie);
        void Save();

    }
    public class MaladieRepository : IMaladieRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public MaladieRepository()
        {
            _context = new ApplicationDbContext();
        }
        public MaladieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Maladie> GetMaladie()
        {
            return _context.Maladies.Include(m => m.CentreSante).Include(m => m.Hopital).ToList();
        }

        public Maladie GetMaladieByID(int Id)
        {
            return _context.Maladies.Find(Id);
        }

        public void InsertMaladie(Maladie maladie)
        {
            _context.Maladies.Add(maladie);
        }

        public void DeleteMaladie(int maladieID)
        {
            Maladie maladie = _context.Maladies.Find(maladieID);
            _context.Maladies.Remove(maladie);
        }

        public void UpdateMaladie(Maladie maladie)
        {
            _context.Entry(maladie).State = EntityState.Modified;
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
