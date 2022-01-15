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
    public interface IInfirmierRepository : IDisposable
    {
        IEnumerable<Infirmier> GetInfirmier();
        Infirmier GetInfirmierByID(string infirmierID);
        void InsertInfirmier(Infirmier infirmier);
        void DeleteInfirmier(string infirmierID);
        void UpdateInfirmier(Infirmier infirmier);
        void Save();

    }
    public class InfirmierRepository : IInfirmierRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public InfirmierRepository()
        {
            _context = new ApplicationDbContext();
        }
        public InfirmierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Infirmier> GetInfirmier()
        {
            return _context.Infirmiers.Include(i => i.Service).ToList();
        }

        public Infirmier GetInfirmierByID(string Id)
        {
            return _context.Infirmiers.Find(Id);
        }

        public void InsertInfirmier(Infirmier infirmier)
        {
            _context.Infirmiers.Add(infirmier);
        }

        public void DeleteInfirmier(string infirmierID)
        {
            Infirmier infirmier = _context.Infirmiers.Find(infirmierID);
            _context.Infirmiers.Remove(infirmier);
        }

        public void UpdateInfirmier(Infirmier infirmier)
        {
            _context.Entry(infirmier).State = EntityState.Modified;
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
