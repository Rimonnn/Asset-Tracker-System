using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using AssetTracker.Models.EntryModel;

namespace AssetTracker.Contex
{
    public class AssetTrackerDB:DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationBranch> OrganizationBranches { get; set; }
    }
}