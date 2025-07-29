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
    public class RoleServices
    {
        private readonly IRepository<Role> _RoleRepository;
        IMapper _mapper;
        public RoleServices(IRepository<Role> RoleRepository, IMapper mapper)
        {
            _RoleRepository = RoleRepository;
            _mapper = mapper;
        }
        public Role GetById(int id)
        {
            var role = _RoleRepository.GetByIdAsync(id).Result;
            if (role == null)
                throw new Exception("Role not found");
            return role;
        }
        // GetAll
        public async Task <IEnumerable<Role>> GetAll()
        {
            return  _RoleRepository.GetAll();
        }
        // Add
        public async Task<bool> AddAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role), "Role cannot be null");
            try
            {
                await _RoleRepository.AddAsync(role);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
        // Update
        public async Task<bool> UpdateAsync(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role), "Role cannot be null");
            try
            {
                await _RoleRepository.UpdateAsync(role);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _RoleRepository.GetByIdAsyncWithTracking(id);
            if (role == null)
                return false;
            try
            {
                await _RoleRepository.DeleteAsync(role);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }
    }
}
