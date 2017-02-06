using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetTracker.Models.EntryModel;
using AssetTracker.Models.Interfaces.BaseInterface;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.Models.Interfaces
{
    public interface IOrganizationRepository:IRepository<Organization>
    {
          
        ICollection<Organization> GetOrganizationBySearchCriteria(OrganizationSearch search);
        string AutoGenerateCode(Organization organization);
        bool IsNameExist(string code);

    }
}
