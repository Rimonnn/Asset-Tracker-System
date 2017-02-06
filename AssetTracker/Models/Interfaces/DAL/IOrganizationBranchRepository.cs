using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTracker.Models.EntryModel;
using AssetTracker.Models.Interfaces.BaseInterface;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.Models.Interfaces.DAL
{
   public interface IOrganizationBranchRepository:IRepository<OrganizationBranch>
   {
       ICollection<OrganizationBranch> GetOrgBranhBySearchCriteria(OrganizationBranchSearchCriteria search);
       string AutoGengerateCode(OrganizationBranch branch);
       bool IsNameExist(string code);
   }
}
