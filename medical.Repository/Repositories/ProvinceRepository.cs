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
    public interface IProvinceRepository : IDisposable
    {
        IEnumerable<Province> GetProvince();
        Province GetProvinceByID(int provinceID);
        void InsertProvince(Province province);
        void DeleteProvince(int provinceID);
        void UpdateProvince(Province province);
        void Save();

    }
    public class ProvinceRepository : IProvinceRepository, IDisposable
    {
        private ApplicationDbContext _context;
        public ProvinceRepository()
        {
            _context = new ApplicationDbContext();
        }
        public ProvinceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Province> GetProvince()
        {
            return _context.Provinces.ToList();
        }

        public Province GetProvinceByID(int Id)
        {
            return _context.Provinces.Find(Id);
        }

        public void InsertProvince(Province province)
        {
            _context.Provinces.Add(province);
        }

        public void DeleteProvince(int provinceID)
        {
            Province province = _context.Provinces.Find(provinceID);
            _context.Provinces.Remove(province);
        }

        public void UpdateProvince(Province province)
        {
            _context.Entry(province).State = EntityState.Modified;
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
