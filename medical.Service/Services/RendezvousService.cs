using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IRendezvousService
    {
        IEnumerable<Rendezvous> GetRendezvous();
        Rendezvous GetRendezvousByID(int rendezvousID);
        void InsertRendezvous(Rendezvous rendezvous);
        void DeleteRendezvous(int rendezvousID);
        void UpdateRendezvous(Rendezvous rendezvous);
        void Save();

    }
    public class RendezvousService : IRendezvousService
    {
        private readonly IRendezvousRepository _rendezvousRepo;

        public RendezvousService()
        {
            _rendezvousRepo = new RendezvousRepository();
        }

        public RendezvousService(IRendezvousRepository rendezvousRepo)
        {
            _rendezvousRepo = rendezvousRepo;
        }

        public IEnumerable<Rendezvous> GetRendezvous() => _rendezvousRepo.GetRendezvous();
        public Rendezvous GetRendezvousByID(int rendezvousID) => _rendezvousRepo.GetRendezvousByID(rendezvousID);
        public void InsertRendezvous(Rendezvous rendezvous) => _rendezvousRepo.InsertRendezvous(rendezvous);
        public void DeleteRendezvous(int rendezvousID) => _rendezvousRepo.DeleteRendezvous(rendezvousID);
        public void UpdateRendezvous(Rendezvous rendezvous) => _rendezvousRepo.UpdateRendezvous(rendezvous);
        public void Save() => _rendezvousRepo.Save();

    }
}
