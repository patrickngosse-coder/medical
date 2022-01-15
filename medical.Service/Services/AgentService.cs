using medical.Model.Models;
using medical.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Service.Services
{

    public interface IAgentService
    {
        IEnumerable<Agent> GetAgent();
        Agent GetAgentByID(string agentID);
        void InsertAgent(Agent agent);
        void DeleteAgent(string agentID);
        void UpdateAgent(Agent agent);
        void Save();

    }
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepo;
        public AgentService()
        {
            _agentRepo = new AgentRepository();
        }
        public AgentService(IAgentRepository agentRepo)
        {
            _agentRepo = agentRepo;
        }

        public IEnumerable<Agent> GetAgent() => _agentRepo.GetAgent();
        public Agent GetAgentByID(string agentID) => _agentRepo.GetAgentByID(agentID);
        public void InsertAgent(Agent agent) => _agentRepo.InsertAgent(agent);
        public void DeleteAgent(string agentID) => _agentRepo.DeleteAgent(agentID);
        public void UpdateAgent(Agent agent) => _agentRepo.UpdateAgent(agent);
        public void Save() => _agentRepo.Save();

    }
}
