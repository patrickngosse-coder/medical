using medical.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace medical.Controllers
{
    public class SearchController : Controller
    {
        private readonly IActeInfiemierService _acteInfirmierService;
        private readonly IActeMedicalService _acteMedicalService;
        private readonly IAgentService _agentService;
        private readonly IServiceService _serveService;
        private readonly IExamenService _examenService;
        private readonly IInfirmierService _infirmierService;
        private readonly IMedecinService _medecinService;
        private readonly IPatientService _patientService;

        public SearchController()
        {
            _acteInfirmierService = new ActeInfiemierService();
            _acteMedicalService = new ActeMedicalService();
            _agentService = new AgentService();
            _serveService = new ServiceService();
            _examenService = new ExamenService();
            _infirmierService = new InfirmierService();
            _medecinService = new MedecinService();
            _patientService = new PatientService();
        }

        public SearchController(IActeInfiemierService acteInfirmierService, IActeMedicalService acteMedicalService, IAgentService agentService, IServiceService serveService, IExamenService examenService, IInfirmierService infirmierService, IMedecinService medecinService, IPatientService patientService)
        {
            _acteInfirmierService = acteInfirmierService;
            _acteMedicalService = acteMedicalService;
            _agentService = agentService;
            _serveService = serveService;
            _examenService = examenService;
            _infirmierService = infirmierService;
            _medecinService = medecinService;
            _patientService = patientService;
        }
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        

       

        

       
       

       
       

       

    }
}