using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberGetDto> CreateAsync(MemberCreateDto dto)
        {
            if (await _unitOfWork.Members.ExistsByUserIdAsync(dto.UserId))
            {
                throw new ArgumentException("This user is already registered as a member.");
            }

            var member = _mapper.Map<Member>(dto);
            await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MemberGetDto>(member);
        }

        public async Task<IEnumerable<MemberGetDto>> GetAllAsync()
        {
            var members = await _unitOfWork.Members.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberGetDto>>(members);
        }

        public async Task<MemberGetDto> GetByIdAsync(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return null;

            return _mapper.Map<MemberGetDto>(member);
        }

        public async Task UpdateAsync(int id, MemberUpdateDto dto)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                throw new KeyNotFoundException($"Member with ID {id} not found.");
            }

            _mapper.Map(dto, member);
            _unitOfWork.Members.Update(member);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                throw new KeyNotFoundException($"Member with ID {id} not found.");
            }

            _unitOfWork.Members.Delete(member);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
