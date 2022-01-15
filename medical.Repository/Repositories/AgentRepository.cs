using medical.Data;
using medical.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Repository.Repositories
{
    public interface IAgentRepository : IDisposable
    {
        IEnumerable<Agent> GetAgent();
        Agent GetAgentByID(string agentID);
        void InsertAgent(Agent agent);
        void DeleteAgent(string agentID);
        void UpdateAgent(Agent agent);
        void Save();

    }
    public class AgentRepository : IAgentRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public AgentRepository()
        {
            _context = new ApplicationDbContext();
        }
        public AgentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Agent> GetAgent()
        {
            return _context.Agents.Include(a => a.Service).ToList();
        }

        public Agent GetAgentByID(string Id)
        {
            return _context.Agents.Find(Id);
        }

        public void InsertAgent(Agent agent)
        {
            _context.Agents.Add(agent);
        }

        public void DeleteAgent(string agentID)
        {
            Agent agent = _context.Agents.Find(agentID);
            _context.Agents.Remove(agent);
        }

        public void UpdateAgent(Agent agent)
        {
            _context.Entry(agent).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
