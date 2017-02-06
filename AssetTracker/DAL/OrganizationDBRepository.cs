using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AssetTracker.Contex;
using AssetTracker.DAL.BaseDAL;
using AssetTracker.Models.EntryModel;
using AssetTracker.Models.Interfaces;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.DAL
{
    public class OrganizationDBRepository:BaseRepository<Organization>,IOrganizationRepository,IDisposable
    {
        private AssetTrackerDB Contex // created upcasting for recognize AssetTrackerDB to the base class
        {
            get
            {
                return _db as AssetTrackerDB;
            }
        }

        public OrganizationDBRepository(DbContext db) : base(new AssetTrackerDB())
        {
            base._db = db;
        }


      
        public ICollection<Organization> GetOrganizationBySearchCriteria(OrganizationSearch search)
        {
            
            var organizations = Contex.Organizations.AsQueryable();


            if (!String.IsNullOrEmpty(search.OrganizationName))
            {
                organizations = organizations.Where(c => c.OrganizationName.ToLower().Contains(search.OrganizationName.ToLower()));
            }

            if (!String.IsNullOrEmpty(search.OrganizationCode))
            {
                organizations = organizations.Where(c => c.OrganizationCode.Equals(search.OrganizationCode));
            }



            return organizations.OrderBy(o => o.OrganizationName).ToList();
        }

        public string AutoGenerateCode(Organization organization)
        {
            for (int i = 0; i < Contex.Organizations.Count(); i++)
            {
                organization.OrganizationCode = organization.OrganizationName + 0 + i;
            }
            return organization.OrganizationCode;
        }

        public bool IsNameExist(string code)
        {
            bool IsExist = Contex.Organizations.Any(c => c.OrganizationName == code);
            if (IsExist)
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