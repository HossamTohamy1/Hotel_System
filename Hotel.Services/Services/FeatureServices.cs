using AutoMapper;
using Hotel.Core.Entities;
using Hotel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Services
{
   public  class FeatureServices
    {
        private readonly IRepository<Feature> _featureRepository;
        IMapper _mapper;
        public FeatureServices(IRepository<Feature> featureRepository, IMapper mapper)
        {
            _featureRepository = featureRepository;
            _mapper = mapper;
        }
        public IEnumerable<Feature> GetAllFeaturesAsync()
        {
            return  _featureRepository.GetAll();
        }
        public Feature GetFeatureById(int id)
        {
            var feature = _featureRepository.GetByIdAsync(id).Result;
            if (feature == null)
            {
                throw new Exception("Feature not found");
            }
            return feature;
        }
        public Feature GetFeatureByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Feature name cannot be null or empty", nameof(name));
            }
            var feature = _featureRepository.Get(f => f.Name == name).FirstOrDefault();
            if (feature == null)
            {
                throw new Exception("Feature not found");
            }
            return feature;
        }

        public async Task<bool> AddFeatureAsync(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature), "Feature cannot be null");
            }
            await _featureRepository.AddAsync(feature);
            return true;
        }
        public async Task<bool> UpdateFeatureAsync(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature), "Feature cannot be null");
            }
            var existingFeature = await _featureRepository.GetByIdAsync(feature.Id);
            if (existingFeature == null)
            {
                throw new Exception("Feature not found");
            }
            await _featureRepository.UpdateAsync(feature);
            return true;
        }
        public async Task<bool> DeleteFeatureAsync(int id)
        {
            var feature = _featureRepository.GetByIdWithTracking(id).FirstOrDefault();
            if (feature == null)
            {
                return false; 
            }

            await _featureRepository.DeleteAsync(feature); 
            return true;
        }
    

    }
}
