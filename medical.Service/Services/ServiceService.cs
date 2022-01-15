using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{
    public interface IServiceService
    {
        IEnumerable<Serve> GetServe();
        Serve GetServeByID(int serveID);
        void InsertServe(Serve serve);
        void DeleteServe(int serveID);
        void UpdateServe(Serve serve);
        void Save();

    }
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serveRepo;

        public ServiceService()
        {
            _serveRepo = new ServiceRepository();
        }
        public ServiceService(IServiceRepository serveRepo)
        {
            _serveRepo = serveRepo;
        }

        public IEnumerable<Serve> GetServe() => _serveRepo.GetService();
        public Serve GetServeByID(int serveID) => _serveRepo.GetServiceByID(serveID);
        public void InsertServe(Serve serve) => _serveRepo.InsertService(serve);
        public void DeleteServe(int serveID) => _serveRepo.DeleteService(serveID);
        public void UpdateServe(Serve serve) => _serveRepo.UpdateService(serve);
        public void Save() => _serveRepo.Save();

    }
}
