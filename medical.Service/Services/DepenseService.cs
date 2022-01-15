using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IDepenseService
    {
        IEnumerable<Depense> GetDepense();
        Depense GetDepenseByID(int depenseID);
        void InsertDepense(Depense depense);
        void DeleteDepense(int depenseID);
        void UpdateDepense(Depense depense);
        void Save();

    }
    public class DepenseService : IDepenseService
    {
        private readonly IDepenseRepository _depenseRepo;
        public DepenseService()
        {
            _depenseRepo = new DepenseRepository();
        }
        public DepenseService(IDepenseRepository depenseRepo)
        {
            _depenseRepo = depenseRepo;
        }

        public IEnumerable<Depense> GetDepense() => _depenseRepo.GetDepense();
        public Depense GetDepenseByID(int depenseID) => _depenseRepo.GetDepenseByID(depenseID);
        public void InsertDepense(Depense depense) => _depenseRepo.InsertDepense(depense);
        public void DeleteDepense(int depenseID) => _depenseRepo.DeleteDepense(depenseID);
        public void UpdateDepense(Depense depense) => _depenseRepo.UpdateDepense(depense);
        public void Save() => _depenseRepo.Save();

    }
}
