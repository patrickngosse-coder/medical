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
    public interface ICompterenduRepository : IDisposable
    {
        IEnumerable<Compterendu> GetCompterendu();
        Compterendu GetCompterenduByID(int compterenduID);
        void InsertCompterendu(Compterendu compterendu);
        void DeleteCompterendu(int compterenduID);
        void UpdateCompterendu(Compterendu compterendu);
        void Save();

    }
    public class CompterenduRepository : ICompterenduRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public CompterenduRepository()
        {
            _context = new ApplicationDbContext();
        }
        public CompterenduRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Compterendu> GetCompterendu()
        {
            return _context.Compterendus.Include(c => c.ActeInfiemier).ToList();
        }

        public Compterendu GetCompterenduByID(int Id)
        {
            return _context.Compterendus.Find(Id);
        }

        public void InsertCompterendu(Compterendu compterendu)
        {
            _context.Compterendus.Add(compterendu);
        }

        public void DeleteCompterendu(int compterenduID)
        {
            Compterendu compterendu = _context.Compterendus.Find(compterenduID);
            _context.Compterendus.Remove(compterendu);
        }

        public void UpdateCompterendu(Compterendu compterendu)
        {
            _context.Entry(compterendu).State = EntityState.Modified;
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
