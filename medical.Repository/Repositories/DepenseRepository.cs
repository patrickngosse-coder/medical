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
    public interface IDepenseRepository : IDisposable
    {
        IEnumerable<Depense> GetDepense();
        Depense GetDepenseByID(int depenseID);
        void InsertDepense(Depense depense);
        void DeleteDepense(int depenseID);
        void UpdateDepense(Depense depense);
        void Save();

    }
    public class DepenseRepository : IDepenseRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public DepenseRepository()
        {
            _context = new ApplicationDbContext();
        }
        public DepenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Depense> GetDepense()
        {
            return _context.Depenses.ToList();
        }

        public Depense GetDepenseByID(int Id)
        {
            return _context.Depenses.Find(Id);
        }

        public void InsertDepense(Depense depense)
        {
            _context.Depenses.Add(depense);
        }

        public void DeleteDepense(int depenseID)
        {
            Depense depense = _context.Depenses.Find(depenseID);
            _context.Depenses.Remove(depense);
        }

        public void UpdateDepense(Depense depense)
        {
            _context.Entry(depense).State = EntityState.Modified;
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
