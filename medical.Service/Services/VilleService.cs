using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IVilleService
    {
        IEnumerable<Ville> GetVille();
        Ville GetVilleByID(int villeID);
        void InsertVille(Ville ville);
        void DeleteVille(int villeID);
        void UpdateVille(Ville ville);
        void Save();

    }
    public class VilleService : IVilleService
    {
        private readonly IVilleRepository _villeRepo;
        public VilleService()
        {
            _villeRepo = new VilleRepository();
        }
        public VilleService(IVilleRepository villeRepo)
        {
            _villeRepo = villeRepo;
        }

        public IEnumerable<Ville> GetVille() => _villeRepo.GetVille();
        public Ville GetVilleByID(int villeID) => _villeRepo.GetVilleByID(villeID);
        public void InsertVille(Ville ville) => _villeRepo.InsertVille(ville);
        public void DeleteVille(int villeID) => _villeRepo.DeleteVille(villeID);
        public void UpdateVille(Ville ville) => _villeRepo.UpdateVille(ville);
        public void Save() => _villeRepo.Save();

    }
}
