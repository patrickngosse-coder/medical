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
    public interface IActeMedicalRepository : IDisposable
    {
        IEnumerable<ActeMedical> GetActeMedical();
        ActeMedical GetActeMedicalByID(int acteMedicalID);
        void InsertActeMedical(ActeMedical acteMedical);
        void DeleteActeMedical(int acteMedicalID);
        void UpdateActeMedical(ActeMedical acteMedical);
        void Save();

    }
    public class ActeMedicalRepository : IActeMedicalRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public ActeMedicalRepository()
        {
            _context = new ApplicationDbContext();
        }

        public ActeMedicalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ActeMedical> GetActeMedical()
        {
            return _context.ActeMedicals.ToList();
        }

        public ActeMedical GetActeMedicalByID(int Id)
        {
            return _context.ActeMedicals.Find(Id);
        }

        public void InsertActeMedical(ActeMedical acteMedical)
        {
            _context.ActeMedicals.Add(acteMedical);
        }

        public void DeleteActeMedical(int acteMedicalID)
        {
            ActeMedical acteMedical = _context.ActeMedicals.Find(acteMedicalID);
            _context.ActeMedicals.Remove(acteMedical);
        }

        public void UpdateActeMedical(ActeMedical acteMedical)
        {
            _context.Entry(acteMedical).State = EntityState.Modified;
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
