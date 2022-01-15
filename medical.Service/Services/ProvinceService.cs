using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IProvinceService
    {
        IEnumerable<Province> GetProvince();
        Province GetProvinceByID(int provinceID);
        void InsertProvince(Province province);
        void DeleteProvince(int provinceID);
        void UpdateProvince(Province province);
        void Save();

    }
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepo;
        public ProvinceService()
        {
            _provinceRepo = new ProvinceRepository();
        }
        public ProvinceService(IProvinceRepository provinceRepo)
        {
            _provinceRepo = provinceRepo;
        }

        public IEnumerable<Province> GetProvince() => _provinceRepo.GetProvince();
        public Province GetProvinceByID(int provinceID) => _provinceRepo.GetProvinceByID(provinceID);
        public void InsertProvince(Province province) => _provinceRepo.InsertProvince(province);
        public void DeleteProvince(int provinceID) => _provinceRepo.DeleteProvince(provinceID);
        public void UpdateProvince(Province province) => _provinceRepo.UpdateProvince(province);
        public void Save() => _provinceRepo.Save();

    }
}
