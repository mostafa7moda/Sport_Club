using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AttendanceGetDto> LogAsync(AttendanceLogDto dto)
        {
            // Verify Member and Section exist? Foreign Key verification usually handled by DB, but cleaner via Service.
            // For now, assuming IDs are valid or letting EF handle FK errors.
            var attendance = _mapper.Map<Attendance>(dto);
            await _unitOfWork.Attendances.AddAsync(attendance);
            await _unitOfWork.SaveChangesAsync();

            // Reload to get navigation properties for DTO if needed using GetById
            // Or simple return if Nav props are not strictly required for the return of Log
             // If we really want names in return, we need to load them.
            // Simplified return for now (names might be null unless we fetch).
            // Let's fetch fully populated entity.
            var created = await _unitOfWork.Attendances.GetByIdAsync(attendance.ID);
            return _mapper.Map<AttendanceGetDto>(created);
        }

        public async Task<IEnumerable<AttendanceGetDto>> GetAllAsync()
        {
            var records = await _unitOfWork.Attendances.GetAllAsync();
            return _mapper.Map<IEnumerable<AttendanceGetDto>>(records);
        }

        public async Task<IEnumerable<AttendanceGetDto>> GetByMemberIdAsync(int memberId)
        {
            var records = await _unitOfWork.Attendances.GetByMemberIdAsync(memberId);
            return _mapper.Map<IEnumerable<AttendanceGetDto>>(records);
        }

        public async Task<IEnumerable<AttendanceGetDto>> GetBySectionIdAsync(int sectionId)
        {
            var records = await _unitOfWork.Attendances.GetBySectionIdAsync(sectionId);
             return _mapper.Map<IEnumerable<AttendanceGetDto>>(records);
        }

        public async Task DeleteAsync(int id)
        {
            var record = await _unitOfWork.Attendances.GetByIdAsync(id);
             if (record == null)
            {
                throw new KeyNotFoundException($"Attendance record with ID {id} not found.");
            }
            _unitOfWork.Attendances.Delete(record);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
