using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscriptionGetDto> CreateAsync(SubscriptionCreateDto dto)
        {
            var subscription = _mapper.Map<Subscription>(dto);
            await _unitOfWork.Subscriptions.AddAsync(subscription);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SubscriptionGetDto>(subscription);
        }

        public async Task<IEnumerable<SubscriptionGetDto>> GetAllAsync()
        {
            var subscriptions = await _unitOfWork.Subscriptions.GetAllAsync();
            return _mapper.Map<IEnumerable<SubscriptionGetDto>>(subscriptions);
        }

        public async Task<SubscriptionGetDto> GetByIdAsync(int id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null) return null;

            return _mapper.Map<SubscriptionGetDto>(subscription);
        }

        public async Task UpdateAsync(int id, SubscriptionUpdateDto dto)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null)
            {
                throw new KeyNotFoundException($"Subscription with ID {id} not found.");
            }

            _mapper.Map(dto, subscription);
            _unitOfWork.Subscriptions.Update(subscription);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subscription = await _unitOfWork.Subscriptions.GetByIdAsync(id);
            if (subscription == null)
            {
                throw new KeyNotFoundException($"Subscription with ID {id} not found.");
            }

            _unitOfWork.Subscriptions.Delete(subscription);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
