using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetTracker.Models.EntryModel
{
    public class OrganizationBranch
    {
        public int Id { get; set; }
        [DisplayName("Organization Branch")]
        [Required(ErrorMessage = "Organization  branch field is requirded")]
        public string OrganizationBranchName{ get; set; }
        public string OrganizationBranchCode { get; set; }
        [DisplayName("Organization")]       
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }


    }
}