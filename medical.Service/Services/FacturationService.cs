using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IFacturationService
    {
        IEnumerable<Facturation> GetFacturation();
        Facturation GetFacturationByID(int facturationID);
        void InsertFacturation(Facturation facturation);
        void DeleteFacturation(int facturationID);
        void UpdateFacturation(Facturation facturation);
        void Save();

    }
    public class FacturationService : IFacturationService
    {
        private readonly IFacturationRepository _facturationRepo;

        public FacturationService()
        {
            _facturationRepo = new FacturationRepository();
        }

        public FacturationService(IFacturationRepository facturationRepo)
        {
            _facturationRepo = facturationRepo;
        }

        public IEnumerable<Facturation> GetFacturation() => _facturationRepo.GetFacturation();
        public Facturation GetFacturationByID(int facturationID) => _facturationRepo.GetFacturationByID(facturationID);
        public void InsertFacturation(Facturation facturation) => _facturationRepo.InsertFacturation(facturation);
        public void DeleteFacturation(int facturationID) => _facturationRepo.DeleteFacturation(facturationID);
        public void UpdateFacturation(Facturation facturation) => _facturationRepo.UpdateFacturation(facturation);
        public void Save() => _facturationRepo.Save();

    }
}
