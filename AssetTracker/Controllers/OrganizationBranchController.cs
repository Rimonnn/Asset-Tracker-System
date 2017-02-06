using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using AssetTracker.Models.EntryModel;
using AssetTracker.Contex;
using AssetTracker.Models.Interfaces;
using AssetTracker.Models.Interfaces.BLL;
using AssetTracker.Models.ViewModel;
using Microsoft.Ajax.Utilities;

namespace AssetTracker.Controllers
{
    public class OrganizationBranchController : Controller
    {
        private IOrganizationBranchManager _branchManager;
        private IOrganizationManager _organizationManager;

        public OrganizationBranchController(IOrganizationBranchManager organizationBranchManager,IOrganizationManager organizationManager)
        {
            _branchManager = organizationBranchManager;
            _organizationManager = organizationManager;
        }

        // Index
        public ActionResult Index()
        {
           
            return View(_branchManager.GetAll().ToList());
        }

        // Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationBranch organizationbranch = _branchManager.GetById((int)id);
            if (organizationbranch == null)
            {
                return HttpNotFound();
            }
            return View(organizationbranch);
        }

        // Create
        public ActionResult Create()
        {
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "OrganizationName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( OrganizationBranch organizationbranch)
        {
            if (ModelState.IsValid)
            {
                bool IsNameExist = _branchManager.IsNameExist(organizationbranch.OrganizationBranchName);
                if (IsNameExist)
                {
                    ViewData["Exist"] = "Name is already Exist !";
                }
                else
                {
                    _branchManager.AutoGengerateCode(organizationbranch);
                    _branchManager.Add(organizationbranch);

                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "OrganizationName", organizationbranch.OrganizationId);
            return View(organizationbranch);
        }

        // Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationBranch organizationbranch = _branchManager.GetById((int) id);
            if (organizationbranch == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "OrganizationName", organizationbranch.OrganizationId);
            return View(organizationbranch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,OrganizationBranchName,OrganizationBranchCode,OrganizationId")] OrganizationBranch organizationbranch)
        {
            if (ModelState.IsValid)
            {
                _branchManager.Update(organizationbranch);
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationId = new SelectList(_organizationManager.GetAll(), "Id", "OrganizationName", organizationbranch.OrganizationId);
            return View(organizationbranch);
        }

        // Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrganizationBranch organizationbranch = _branchManager.GetById((int) id);
            if (organizationbranch == null)
            {
                return HttpNotFound();
            }
            return View(organizationbranch);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var organizationbranch = _branchManager.GetById((int) id);
            _branchManager.Delete(organizationbranch);
            return RedirectToAction("Index");
        }

        public ActionResult Search()
        {
            var organizationBranch = _branchManager.GetAll();
            return View(organizationBranch);
        }

        [HttpPost]
        public ActionResult Search(OrganizationBranchSearchCriteria branchsearch)
        {
            var organizationBranch = _branchManager.GetOrgBranhBySearchCriteria(branchsearch);
            return View(organizationBranch);
        }
        public JsonResult GetAllOrganizationBranch(string draw, int? start, int? length)
        {
            AssetTrackerDB db = new AssetTrackerDB();

            int initial = start ?? 0;
            int totalPages = length ?? 0;

            var totalRecords = db.OrganizationBranches.Count();

            var organizationList = db.OrganizationBranches.OrderBy(c => c.OrganizationBranchName).Skip(initial).Take(totalPages);


            var organizationBranches = organizationList.Select(c => new { Id = c.Id, OrganizationBranchName = c.OrganizationBranchName, OrganizationBranchCode = c.OrganizationBranchCode }).ToList();
            var jsonData =
                new { draw = draw, recordsTotal = totalRecords, recordsFiltered = totalRecords, data = organizationBranches };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
       
    }
}
