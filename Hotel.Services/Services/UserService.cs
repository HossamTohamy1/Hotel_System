using Hotel.Core.Entities;
using Hotel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Services.Interfaces;
using AutoMapper;
namespace Hotel.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Offer> _repository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> PromoteCustomerToStaffAsync(CustomerToStaffPromotionDto promotionDto)
        {
            try
            {
                var customer = await _userRepository.GetCustomerByIdAsync(promotionDto.CustomerId);
                if (customer == null)
                    throw new Exception($"Customer with ID {promotionDto.CustomerId} not found");

                var staff = _mapper.Map<Staff>(promotionDto);

                await _userRepository.PromoteCustomerToStaffAsync(customer, staff);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}