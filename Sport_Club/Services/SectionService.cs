using AutoMapper;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Services
{
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SectionGetDto> CreateAsync(SectionCreateDto dto)
        {
            var section = _mapper.Map<Section>(dto);
            await _unitOfWork.Sections.AddAsync(section);
            await _unitOfWork.SaveChangesAsync();

            // Fetch to ensure includes if needed, or just map back
            return _mapper.Map<SectionGetDto>(section);
        }

        public async Task<IEnumerable<SectionGetDto>> GetAllAsync()
        {
            var sections = await _unitOfWork.Sections.GetAllAsync();
            return _mapper.Map<IEnumerable<SectionGetDto>>(sections);
        }

        public async Task<SectionGetDto> GetByIdAsync(int id)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(id);
            if (section == null) return null;

            return _mapper.Map<SectionGetDto>(section);
        }

        public async Task UpdateAsync(int id, SectionUpdateDto dto)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(id);
            if (section == null)
            {
                throw new KeyNotFoundException($"Section with ID {id} not found.");
            }

            _mapper.Map(dto, section);
            _unitOfWork.Sections.Update(section);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var section = await _unitOfWork.Sections.GetByIdAsync(id);
            if (section == null)
            {
                throw new KeyNotFoundException($"Section with ID {id} not found.");
            }

            _unitOfWork.Sections.Delete(section);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
