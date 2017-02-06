using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AssetTracker.Contex;
using AssetTracker.DAL.BaseDAL;
using AssetTracker.Models.EntryModel;
using AssetTracker.Models.Interfaces.DAL;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.DAL
{
    public class OrganizationBranhDBRepository:BaseRepository<OrganizationBranch>,IOrganizationBranchRepository,IDisposable
    {
        private AssetTrackerDB Contex 
        {
            get
            {
                return _db as AssetTrackerDB;
            }
        }
        public OrganizationBranhDBRepository(DbContext db) : base(db)
        {
            _db = db;
        }

       

        public ICollection<OrganizationBranch> GetOrgBranhBySearchCriteria(OrganizationBranchSearchCriteria search)
        {
            var OrgBranches = Contex.OrganizationBranches.AsQueryable();
            if (!string.IsNullOrEmpty(search.OrgBranchName))
            {
                OrgBranches =
                    OrgBranches.Where(c => c.OrganizationBranchName.ToLower().Contains(search.OrgBranchName.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.OrgBranchCode))
            {
                OrgBranches = OrgBranches.Where(c => c.OrganizationBranchCode.Equals(search.OrgBranchCode));
            }
            return OrgBranches.OrderBy(c => c.OrganizationBranchName).ToList();
        }

        public string AutoGengerateCode(OrganizationBranch branch)
        {
            for (int i = 0; i < Contex.OrganizationBranches.Count(); i++)
            {
                branch.OrganizationBranchCode = branch.OrganizationBranchName + 0 + i;
                
            }
            return branch.OrganizationBranchCode;
            
                                      
            
        }

        public bool IsNameExist(string code)
        {
            bool IsNameExist = Contex.OrganizationBranches.Any(c => c.OrganizationBranchName == code);
            if (IsNameExist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            Contex.Dispose();
        }
    }
}