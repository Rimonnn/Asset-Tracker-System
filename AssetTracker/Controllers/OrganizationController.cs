using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssetTracker.Models.EntryModel;
using AssetTracker.Contex;
using AssetTracker.DAL.BaseDAL;
using AssetTracker.Models.Interfaces;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.Controllers
{
    public class OrganizationController : Controller
    {
        #region
        private IOrganizationManager _organizationManager;
       
       

        public OrganizationController(IOrganizationManager organizationManager)
        {
            _organizationManager = organizationManager;
           

        }
        #endregion
        //Index
        #region

        public ActionResult Index()
        {
            return View(_organizationManager.GetAll().ToList());
        }
        #endregion
        // Details
        #region
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = _organizationManager.GetById((int)id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }
        #endregion

        // Create
        #region
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrganizationName,OrganizationBranchCode")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                bool IsNameExist = _organizationManager.IsNameExist(organization.OrganizationName);
                if (IsNameExist)
                {
                    ViewData["Exist"] = "Organization name is already exist !";
                }
                else
                {
                    _organizationManager.AutoGenerateCode(organization);
                    _organizationManager.Add(organization);

                    return RedirectToAction("Index");
                }
                
            }

            return View(organization);
        }
        #endregion

        //Edit
        #region
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = _organizationManager.GetById((int)id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrganizationName,OrganizationBranchCode")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                _organizationManager.Update(organization);

                return RedirectToAction("Index");
            }
            return View(organization);
        }
        #endregion


        // Delete
        #region
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = _organizationManager.GetById((int)id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            return View(organization);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organization organization = _organizationManager.GetById(id);
            bool isDelete = _organizationManager.Delete(organization);
            if (isDelete)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        #endregion


        //Search
        #region
        public ActionResult Search()
        {
            var organization = _organizationManager.GetAll();
            return View(organization);
        }

        [HttpPost]
        public ActionResult Search(OrganizationSearch search)
        {
           var organizations = _organizationManager.GetOrganizationBySearchCriteria(search);
           return View(organizations);
        }
        
      

        
        public JsonResult GetAllOrganization(string draw, int? start, int? length)
        {
            AssetTrackerDB db=new AssetTrackerDB();
       
            int initial = start ?? 0;
            int totalPages = length ?? 0;

            var totalRecords = db.Organizations.Count();

            var organizationList = db.Organizations.OrderBy(c => c.OrganizationName).Skip(initial).Take(totalPages);


            var organizations = organizationList.Select(c => new { Id = c.Id, OrganizationName = c.OrganizationName, OrganizationCode = c.OrganizationCode}).ToList();
            var jsonData =
                new { draw = draw, recordsTotal = totalRecords, recordsFiltered = totalRecords, data = organizations };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
    }
}
