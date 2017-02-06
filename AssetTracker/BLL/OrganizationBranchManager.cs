using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AssetTracker.Models.EntryModel;
using AssetTracker.Models.Interfaces;
using AssetTracker.Models.Interfaces.BLL;
using AssetTracker.Models.Interfaces.DAL;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.BLL
{
    public class OrganizationBranchManager:IOrganizationBranchManager
    {
        private IOrganizationBranchRepository _repository;

        public OrganizationBranchManager(IOrganizationBranchRepository repository)
        {
            this._repository = repository;
        }
        public bool Add(OrganizationBranch entity)
        {
            return _repository.Add(entity);
        }

        public bool Update(OrganizationBranch entity)
        {
            return _repository.Update(entity);
        }

        public bool Delete(OrganizationBranch entity)
        {
            return _repository.Delete(entity);
        }

        public ICollection<OrganizationBranch> GetAll()
        {
            return _repository.GetAll();
        }

        public OrganizationBranch GetById(int id)
        {
            return _repository.GetById(id);
        }

        public OrganizationBranch GetByCode(string code)
        {
            return _repository.Get(c => c.OrganizationBranchCode == code).FirstOrDefault();
        }

        public ICollection<OrganizationBranch> GetOrgBranhBySearchCriteria(OrganizationBranchSearchCriteria search)
        {
            return _repository.GetOrgBranhBySearchCriteria(search);
        }

        public string AutoGengerateCode(OrganizationBranch branch)
        {
            return _repository.AutoGengerateCode(branch);
        }

        public bool IsNameExist(string code)
        {
           return _repository.IsNameExist(code);
        }
    }
}