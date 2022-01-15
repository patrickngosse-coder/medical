using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IZoneSanteService
    {
        IEnumerable<ZoneSante> GetZoneSante();
        ZoneSante GetZoneSanteByID(int zoneSanteID);
        void InsertZoneSante(ZoneSante zoneSante);
        void DeleteZoneSante(int zoneSanteID);
        void UpdateZoneSante(ZoneSante zoneSante);
        void Save();

    }
    public class ZoneSanteService : IZoneSanteService
    {
        private readonly IZoneSanteRepository _zoneSanteRepo;
        public ZoneSanteService()
        {
            _zoneSanteRepo = new ZoneSanteRepository();
        }
        public ZoneSanteService(IZoneSanteRepository zoneSanteRepo)
        {
            _zoneSanteRepo = zoneSanteRepo;
        }

        public IEnumerable<ZoneSante> GetZoneSante() => _zoneSanteRepo.GetZoneSante();
        public ZoneSante GetZoneSanteByID(int zoneSanteID) => _zoneSanteRepo.GetZoneSanteByID(zoneSanteID);
        public void InsertZoneSante(ZoneSante zoneSante) => _zoneSanteRepo.InsertZoneSante(zoneSante);
        public void DeleteZoneSante(int zoneSanteID) => _zoneSanteRepo.DeleteZoneSante(zoneSanteID);
        public void UpdateZoneSante(ZoneSante zoneSante) => _zoneSanteRepo.UpdateZoneSante(zoneSante);
        public void Save() => _zoneSanteRepo.Save();

    }
}
