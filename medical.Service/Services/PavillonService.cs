using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IPavillonService
    {
        IEnumerable<Pavillon> GetPavillon();
        Pavillon GetPavillonByID(int pavillonID);
        void InsertPavillon(Pavillon pavillon);
        void DeletePavillon(int pavillonID);
        void UpdatePavillon(Pavillon pavillon);
        void Save();

    }
    public class PavillonService : IPavillonService
    {
        private readonly IPavillonRepository _pavillonRepo;
        public PavillonService()
        {
            _pavillonRepo = new PavillonRepository();
        }
        public PavillonService(IPavillonRepository pavillonRepo)
        {
            _pavillonRepo = pavillonRepo;
        }

        public IEnumerable<Pavillon> GetPavillon() => _pavillonRepo.GetPavillon();
        public Pavillon GetPavillonByID(int pavillonID) => _pavillonRepo.GetPavillonByID(pavillonID);
        public void InsertPavillon(Pavillon pavillon) => _pavillonRepo.InsertPavillon(pavillon);
        public void DeletePavillon(int pavillonID) => _pavillonRepo.DeletePavillon(pavillonID);
        public void UpdatePavillon(Pavillon pavillon) => _pavillonRepo.UpdatePavillon(pavillon);
        public void Save() => _pavillonRepo.Save();

    }
}
