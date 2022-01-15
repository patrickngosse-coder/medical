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
    public interface IZoneSanteRepository : IDisposable
    {
        IEnumerable<ZoneSante> GetZoneSante();
        ZoneSante GetZoneSanteByID(int zoneSanteID);
        void InsertZoneSante(ZoneSante zoneSante);
        void DeleteZoneSante(int zoneSanteID);
        void UpdateZoneSante(ZoneSante zoneSante);
        void Save();

    }
    public class ZoneSanteRepository : IZoneSanteRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public ZoneSanteRepository()
        {
            _context = new ApplicationDbContext();
        }
        public ZoneSanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ZoneSante> GetZoneSante()
        {
            return _context.ZoneSantes.Include(z => z.DivisionProvincialSante).ToList();
        }

        public ZoneSante GetZoneSanteByID(int Id)
        {
            return _context.ZoneSantes.Find(Id);
        }

        public void InsertZoneSante(ZoneSante zoneSante)
        {
            _context.ZoneSantes.Add(zoneSante);
        }

        public void DeleteZoneSante(int zoneSanteID)
        {
            ZoneSante zoneSante = _context.ZoneSantes.Find(zoneSanteID);
            _context.ZoneSantes.Remove(zoneSante);
        }

        public void UpdateZoneSante(ZoneSante zoneSante)
        {
            _context.Entry(zoneSante).State = EntityState.Modified;
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
