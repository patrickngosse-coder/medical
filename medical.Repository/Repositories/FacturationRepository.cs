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
    public interface IFacturationRepository : IDisposable
    {
        IEnumerable<Facturation> GetFacturation();
        Facturation GetFacturationByID(int facturationID);
        void InsertFacturation(Facturation facturation);
        void DeleteFacturation(int facturationID);
        void UpdateFacturation(Facturation facturation);
        void Save();

    }
    public class FacturationRepository : IFacturationRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public FacturationRepository()
        {
            _context = new ApplicationDbContext();
        }
        public FacturationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Facturation> GetFacturation()
        {
            return _context.Facturations.ToList();
        }

        public Facturation GetFacturationByID(int Id)
        {
            return _context.Facturations.Find(Id);
        }

        public void InsertFacturation(Facturation facturation)
        {
            _context.Facturations.Add(facturation);
        }

        public void DeleteFacturation(int facturationID)
        {
            Facturation facturation = _context.Facturations.Find(facturationID);
            _context.Facturations.Remove(facturation);
        }

        public void UpdateFacturation(Facturation facturation)
        {
            _context.Entry(facturation).State = EntityState.Modified;
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
