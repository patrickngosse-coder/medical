using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IHopitalService
    {
        IEnumerable<Hopital> GetHopital();
        Hopital GetHopitalByID(int hopitalID);
        void InsertHopital(Hopital hopital);
        void DeleteHopital(int hopitalID);
        void UpdateHopital(Hopital hopital);
        void Save();

    }
    public class HopitalService : IHopitalService
    {
        private readonly IHopitalRepository _hopitalRepo;

        public HopitalService()
        {
            _hopitalRepo = new HopitalRepository();
        }

        public HopitalService(IHopitalRepository hopitalRepo)
        {
            _hopitalRepo = hopitalRepo;
        }

        public IEnumerable<Hopital> GetHopital() => _hopitalRepo.GetHopital();
        public Hopital GetHopitalByID(int hopitalID) => _hopitalRepo.GetHopitalByID(hopitalID);
        public void InsertHopital(Hopital hopital) => _hopitalRepo.InsertHopital(hopital);
        public void DeleteHopital(int hopitalID) => _hopitalRepo.DeleteHopital(hopitalID);
        public void UpdateHopital(Hopital hopital) => _hopitalRepo.UpdateHopital(hopital);
        public void Save() => _hopitalRepo.Save();

    }
}
