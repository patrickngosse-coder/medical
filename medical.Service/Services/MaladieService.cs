using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IMaladieService
    {
        IEnumerable<Maladie> GetMaladie();
        Maladie GetMaladieByID(int maladieID);
        void InsertMaladie(Maladie maladie);
        void DeleteMaladie(int maladieID);
        void UpdateMaladie(Maladie maladie);
        void Save();

    }
    public class MaladieService : IMaladieService
    {
        private readonly IMaladieRepository _maladieRepo;

        public MaladieService()
        {
            _maladieRepo = new MaladieRepository();
        }

        public MaladieService(IMaladieRepository maladieRepo)
        {
            _maladieRepo = maladieRepo;
        }

        public IEnumerable<Maladie> GetMaladie() => _maladieRepo.GetMaladie();
        public Maladie GetMaladieByID(int maladieID) => _maladieRepo.GetMaladieByID(maladieID);
        public void InsertMaladie(Maladie maladie) => _maladieRepo.InsertMaladie(maladie);
        public void DeleteMaladie(int maladieID) => _maladieRepo.DeleteMaladie(maladieID);
        public void UpdateMaladie(Maladie maladie) => _maladieRepo.UpdateMaladie(maladie);
        public void Save() => _maladieRepo.Save();

    }
}
