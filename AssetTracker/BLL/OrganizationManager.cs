using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AssetTracker.DAL;
using AssetTracker.Models.EntryModel;
using AssetTracker.Models.Interfaces;
using AssetTracker.Models.ViewModel;

namespace AssetTracker.BLL
{
    public class OrganizationManager:IOrganizationManager
    {
        private IOrganizationRepository _repository;

        

        public OrganizationManager(IOrganizationRepository repository)//when you have another repository you can use like this..
        {
           this._repository = repository;
        }
        public bool Add(Organization organization)
        {
            return _repository.Add(organization);
        }

        public bool Update(Organization organization)
        {
            return _repository.Update(organization);
        }

        public bool Delete(Organization organization)
        {
            return _repository.Delete(organization);
        }

        public ICollection<Organization> GetAll()
        {
            return _repository.GetAll();
        }

        public Organization GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ICollection<Organization> GetOrganizationBySearchCriteria(OrganizationSearch search)
        {
            return _repository.GetOrganizationBySearchCriteria(search);
        }

        public string AutoGenerateCode(Organization organization)
        {
          return  _repository.AutoGenerateCode(organization);
        }

        public bool IsNameExist(string code)
        {
            return _repository.IsNameExist(code);
        }

        public Organization GetByCode(string code)
        {
            return _repository.Get(c => c.OrganizationCode == code).FirstOrDefault();
        }
    }
}