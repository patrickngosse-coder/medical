using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IActeInfiemierService
    {
        IEnumerable<ActeInfiemier> GetActeInfiemier();
        ActeInfiemier GetActeInfiemierByID(int acteInfiemierID);
        void InsertActeInfiemier(ActeInfiemier acteInfiemier);
        void DeleteActeInfiemier(int acteInfiemierID);
        void UpdateActeInfiemier(ActeInfiemier acteInfiemier);
        void Save();

    }
    public class ActeInfiemierService: IActeInfiemierService
    {
        private readonly IActeInfiemierRepository _acteInfiemierRepo;

        public ActeInfiemierService()
        {
            _acteInfiemierRepo = new ActeInfiemierRepository();
        }

        public ActeInfiemierService(IActeInfiemierRepository acteInfiemierRepo)
        {
            _acteInfiemierRepo = acteInfiemierRepo;
        }

        public IEnumerable<ActeInfiemier> GetActeInfiemier() => _acteInfiemierRepo.GetActeInfiemier();
        public ActeInfiemier GetActeInfiemierByID(int acteInfiemierID) => _acteInfiemierRepo.GetActeInfiemierByID(acteInfiemierID);
        public void InsertActeInfiemier(ActeInfiemier acteInfiemier) => _acteInfiemierRepo.InsertActeInfiemier(acteInfiemier);
        public void DeleteActeInfiemier(int acteInfiemierID) => _acteInfiemierRepo.DeleteActeInfiemier(acteInfiemierID);
        public void UpdateActeInfiemier(ActeInfiemier acteInfiemier) => _acteInfiemierRepo.UpdateActeInfiemier(acteInfiemier);
        public void Save() => _acteInfiemierRepo.Save();

    }
}
