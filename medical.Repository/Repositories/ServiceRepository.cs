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
    public interface IServiceRepository : IDisposable
    {
        IEnumerable<Serve> GetService();
        Serve GetServiceByID(int serviceID);
        void InsertService(Serve service);
        void DeleteService(int serviceID);
        void UpdateService(Serve service);
        void Save();

    }
    public class ServiceRepository : IServiceRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository()
        {
            _context = new ApplicationDbContext();
        }
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Serve> GetService()
        {
            return _context.Services.ToList();
        }

        public Serve GetServiceByID(int Id)
        {
            return _context.Services.Find(Id);
        }

        public void InsertService(Serve service)
        {
            _context.Services.Add(service);
        }

        public void DeleteService(int serviceID)
        {
            Serve service = _context.Services.Find(serviceID);
            _context.Services.Remove(service);
        }

        public void UpdateService(Serve service)
        {
            _context.Entry(service).State = EntityState.Modified;
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
