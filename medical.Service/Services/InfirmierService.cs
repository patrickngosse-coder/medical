using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IInfirmierService
    {
        IEnumerable<Infirmier> GetInfirmier();
        Infirmier GetInfirmierByID(string infirmierID);
        void InsertInfirmier(Infirmier infirmier);
        void DeleteInfirmier(string infirmierID);
        void UpdateInfirmier(Infirmier infirmier);
        void Save();

    }
    public class InfirmierService : IInfirmierService
    {
        private readonly IInfirmierRepository _infirmierRepo;

        public InfirmierService()
        {
            _infirmierRepo = new InfirmierRepository();
        }
        public InfirmierService(IInfirmierRepository infirmierRepo)
        {
            _infirmierRepo = infirmierRepo;
        }

        public IEnumerable<Infirmier> GetInfirmier() => _infirmierRepo.GetInfirmier();
        public Infirmier GetInfirmierByID(string infirmierID) => _infirmierRepo.GetInfirmierByID(infirmierID);
        public void InsertInfirmier(Infirmier infirmier) => _infirmierRepo.InsertInfirmier(infirmier);
        public void DeleteInfirmier(string infirmierID) => _infirmierRepo.DeleteInfirmier(infirmierID);
        public void UpdateInfirmier(Infirmier infirmier) => _infirmierRepo.UpdateInfirmier(infirmier);
        public void Save() => _infirmierRepo.Save();

    }
}
