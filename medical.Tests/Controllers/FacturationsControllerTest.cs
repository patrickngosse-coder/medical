using medical.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace medical.Tests.Controllers
{
    [TestClass]
    public class FacturationsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FacturationIndex()
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.FacturationIndex() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Recette()
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.Recette() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Caisse()
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.Caisse() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SoldeMedecin()
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.SoldeMedecin() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CumuleJournalier()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.CumuleJournalier() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CumuleHebdo()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.CumuleHebdo() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EtatDemandeur()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.EtatDemandeur() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EtatExaminateur()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.EtatExaminateur() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EtatPrincipal()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.EtatPrincipal() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EtatSecondaire()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.EtatSecondaire() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EtatCourant()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.EtatCourant() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EtatPatient()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.EtatPatient() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Patients()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.Patients() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ListPatient()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.ListPatient() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MyHonoraires()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.MyHonoraires() as PartialViewResult;

            Assert.IsNotNull(result);
        }


        

        [TestMethod]
        public void Details(int id)
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.Details(id) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            FacturationsController controller = new FacturationsController();

            PartialViewResult result = controller.Create() as PartialViewResult;

            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void Edit(int id)
        //{
        //    FacturationsController controller = new FacturationsController();

        //    ViewResult result = controller.Edit(id) as ViewResult;

        //    Assert.IsNotNull(result);
        //}


        [TestMethod]
        public void Delete(int id)
        {
            FacturationsController controller = new FacturationsController();

            ViewResult result = controller.Delete(id) as ViewResult;

            Assert.IsNotNull(result);
        }

    }
}
