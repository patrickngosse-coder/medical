using medical.Data;
using medical.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Repository.Repositories
{
    public interface IActeInfiemierRepository:IDisposable
    {
        IEnumerable<ActeInfiemier> GetActeInfiemier();
        ActeInfiemier GetActeInfiemierByID(int acteInfiemierID);
        void InsertActeInfiemier(ActeInfiemier acteInfiemier);
        void DeleteActeInfiemier(int acteInfiemierID);
        void UpdateActeInfiemier(ActeInfiemier acteInfiemier);
        void Save();

    }
    public class ActeInfiemierRepository:IActeInfiemierRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public ActeInfiemierRepository()
        {
            _context = new ApplicationDbContext();
        }
        public ActeInfiemierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ActeInfiemier> GetActeInfiemier()
        {
            return _context.ActeInfiemiers.ToList();
        }

        public ActeInfiemier GetActeInfiemierByID(int Id)
        {
            return _context.ActeInfiemiers.Find(Id);

        }

        public void InsertActeInfiemier(ActeInfiemier acteInfiemier)
        {
            _context.ActeInfiemiers.Add(acteInfiemier);
        }

        public void DeleteActeInfiemier(int acteInfiemierID)
        {
            ActeInfiemier acteInfiemier = _context.ActeInfiemiers.Find(acteInfiemierID);
            _context.ActeInfiemiers.Remove(acteInfiemier);
        }

        public void UpdateActeInfiemier(ActeInfiemier acteInfiemier)
        {
            _context.Entry(acteInfiemier).State = EntityState.Modified;
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
