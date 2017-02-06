using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetTracker.Models.EntryModel
{
    public class Organization
    {
        public int Id { get; set; }
        [DisplayName("Organization Name")]
        [Required(ErrorMessage = "Organization Name field is required")]
        public string OrganizationName { get; set; }
        [DisplayName("Organization Code")]
        public string OrganizationCode { get; set; }

        public virtual List<OrganizationBranch> OrganizationBranches { get; set; }

    }
}