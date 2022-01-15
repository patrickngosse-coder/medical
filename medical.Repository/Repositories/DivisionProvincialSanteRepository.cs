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
    public interface IDivisionProvincialSanteRepository : IDisposable
    {
        IEnumerable<DivisionProvincialSante> GetDivisionProvincialSante();
        DivisionProvincialSante GetDivisionProvincialSanteByID(int divisionProvincialSanteID);
        void InsertDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante);
        void DeleteDivisionProvincialSante(int divisionProvincialSanteID);
        void UpdateDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante);
        void Save();

    }
    public class DivisionProvincialSanteRepository : IDivisionProvincialSanteRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public DivisionProvincialSanteRepository()
        {
            _context = new ApplicationDbContext();
        }
        public DivisionProvincialSanteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DivisionProvincialSante> GetDivisionProvincialSante()
        {
            return _context.DivisionProvincialSantes.Include(d => d.Province).ToList();
        }

        public DivisionProvincialSante GetDivisionProvincialSanteByID(int Id)
        {
            return _context.DivisionProvincialSantes.Find(Id);
        }

        public void InsertDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante)
        {
            _context.DivisionProvincialSantes.Add(divisionProvincialSante);
        }

        public void DeleteDivisionProvincialSante(int divisionProvincialSanteID)
        {
            DivisionProvincialSante divisionProvincialSante = _context.DivisionProvincialSantes.Find(divisionProvincialSanteID);
            _context.DivisionProvincialSantes.Remove(divisionProvincialSante);
        }

        public void UpdateDivisionProvincialSante(DivisionProvincialSante divisionProvincialSante)
        {
            _context.Entry(divisionProvincialSante).State = EntityState.Modified;
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

