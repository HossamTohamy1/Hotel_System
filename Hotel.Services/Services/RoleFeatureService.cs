using Hotel.Core.Entities;
using Hotel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services
{
    public class RoleFeatureService
    {
        private readonly IRepository<RoleFeature> _roleFeatureRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Feature> _featureRepository;
        public RoleFeatureService(IRepository<RoleFeature> roleFeatureRepository, IRepository<Role> roleRepository, IRepository<Feature> featureRepository)
        {
            _featureRepository = featureRepository;
            _roleFeatureRepository = roleFeatureRepository;
            _roleRepository = roleRepository;
        }

        public async Task<RoleFeature> GetByIdAsync(int id)
        {
            return await _roleFeatureRepository.GetByIdAsync(id);
        }

        public async Task AssignFeatureToRole(string roleName, string featureName)
        {
            var role = _roleRepository.Get(r => r.Name == roleName).FirstOrDefault();
            var feature = _featureRepository.Get(f => f.Name == featureName).FirstOrDefault();

            if (role == null || feature == null)
                throw new Exception("Role or Feature not found.");

            bool exists = _roleFeatureRepository.Get(rf =>
                rf.RoleId == role.Id && rf.FeatureId == feature.Id).Any();

            if (exists)
                throw new Exception("Feature already assigned to role.");

            var roleFeature = new RoleFeature
            {
                RoleId = role.Id,
                FeatureId = feature.Id
            };

          await  _roleFeatureRepository.AddAsync(roleFeature);
        }

        public bool CheckFeatureAccess(int roleId, int featureId)
        {
            return _roleFeatureRepository
                .Get(rf => rf.RoleId == roleId && rf.FeatureId == featureId)
                .Any();
        }



        public void UpdateFeatureForRole(string roleName, string oldFeatureName, string newFeatureName)
        {
            var role = _roleRepository.Get(r => r.Name == roleName).FirstOrDefault();
            if (role == null)
                throw new Exception("Role not found.");

            var oldFeature = _featureRepository.Get(f => f.Name == oldFeatureName).FirstOrDefault();
            var newFeature = _featureRepository.Get(f => f.Name == newFeatureName).FirstOrDefault();

            if (oldFeature == null || newFeature == null)
                throw new Exception("One or both features not found.");

            var roleFeature = _roleFeatureRepository.Get(rf =>
                rf.RoleId == role.Id && rf.FeatureId == oldFeature.Id).FirstOrDefault();

            if (roleFeature == null)
                throw new Exception("This feature is not assigned to the role.");

            roleFeature.FeatureId = newFeature.Id;
            _roleFeatureRepository.UpdatePartialAsync(roleFeature, nameof(RoleFeature.FeatureId));
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _roleFeatureRepository.GetByIdAsync(id);
            if (entity == null) return false;

            await _roleFeatureRepository.DeleteAsync(entity);
            return true;
        }
    }

}

