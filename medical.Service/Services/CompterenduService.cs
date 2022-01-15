using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface ICompterenduService
    {
        IEnumerable<Compterendu> GetCompterendu();
        Compterendu GetCompterenduByID(int compterenduID);
        void InsertCompterendu(Compterendu compterendu);
        void DeleteCompterendu(int compterenduID);
        void UpdateCompterendu(Compterendu compterendu);
        void Save();

    }
    public class CompterenduService : ICompterenduService
    {
        private readonly ICompterenduRepository _compterenduRepo;

        public CompterenduService()
        {
            _compterenduRepo = new CompterenduRepository();
        }
        public CompterenduService(ICompterenduRepository compterenduRepo)
        {
            _compterenduRepo = compterenduRepo;
        }

        public IEnumerable<Compterendu> GetCompterendu() => _compterenduRepo.GetCompterendu();
        public Compterendu GetCompterenduByID(int compterenduID) => _compterenduRepo.GetCompterenduByID(compterenduID);
        public void InsertCompterendu(Compterendu compterendu) => _compterenduRepo.InsertCompterendu(compterendu);
        public void DeleteCompterendu(int compterenduID) => _compterenduRepo.DeleteCompterendu(compterenduID);
        public void UpdateCompterendu(Compterendu compterendu) => _compterenduRepo.UpdateCompterendu(compterendu);
        public void Save() => _compterenduRepo.Save();

    }
}

