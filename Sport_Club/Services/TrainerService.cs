using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TrainerGetDto> CreateAsync(TrainerCreateDto dto)
        {
            if (await _unitOfWork.Trainers.ExistsByUserIdAsync(dto.UserId))
            {
                throw new ArgumentException("This user is already registered as a trainer.");
            }

            var trainer = _mapper.Map<Trainer>(dto);
            await _unitOfWork.Trainers.AddAsync(trainer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TrainerGetDto>(trainer);
        }

        public async Task<IEnumerable<TrainerGetDto>> GetAllAsync()
        {
            var trainers = await _unitOfWork.Trainers.GetAllAsync();
            return _mapper.Map<IEnumerable<TrainerGetDto>>(trainers);
        }

        public async Task<TrainerGetDto> GetByIdAsync(int id)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(id);
            if (trainer == null) return null;

            return _mapper.Map<TrainerGetDto>(trainer);
        }

        public async Task UpdateAsync(int id, TrainerUpdateDto dto)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(id);
            if (trainer == null)
            {
                throw new KeyNotFoundException($"Trainer with ID {id} not found.");
            }

            _mapper.Map(dto, trainer);
            _unitOfWork.Trainers.Update(trainer);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trainer = await _unitOfWork.Trainers.GetByIdAsync(id);
            if (trainer == null)
            {
                throw new KeyNotFoundException($"Trainer with ID {id} not found.");
            }

            _unitOfWork.Trainers.Delete(trainer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
