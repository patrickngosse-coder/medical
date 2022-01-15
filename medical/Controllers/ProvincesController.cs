using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using medical.Data;
using medical.Model.Models;
using medical.Service.Services;

namespace medical.Controllers
{
    [Authorize]
    public class ProvincesController : Controller
    {
        private readonly IProvinceService _provinceService;

        public ProvincesController()
        {
            _provinceService = new ProvinceService();
        }

        public ProvincesController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        // GET: Provinces
        public ActionResult Index()
        {
            var item = from s in _provinceService.GetProvince() select s;
            return View(item.ToList());
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = _provinceService.GetProvinceByID(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // GET: Provinces/Create
        public PartialViewResult Create()
        {
            return PartialView();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDPROVINCE,DESIGNATION")] Province province)
        {
            if (ModelState.IsValid)
            {
               _provinceService.InsertProvince(province);
               _provinceService.Save();
                return RedirectToAction("Index");
            }

            return View(province);
        }

        // GET: Provinces/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = _provinceService.GetProvinceByID(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPROVINCE,DESIGNATION")] Province province)
        {
            if (ModelState.IsValid)
            {
                _provinceService.UpdateProvince(province);
                _provinceService.Save();
                return RedirectToAction("Index");
            }
            return View(province);
        }

        // GET: Provinces/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = _provinceService.GetProvinceByID(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Province province = _provinceService.GetProvinceByID(id);
           _provinceService.DeleteProvince(id);
           _provinceService.Save();
            return RedirectToAction("Index");
        }

    }
}
