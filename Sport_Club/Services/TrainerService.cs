//using AutoMapper;
//using Sport_Club.DTOs;
//using Sport_Club.Interfaces;
//using Sport_Club.Models;

//namespace Sport_Club.Services
//{
//    public class TrainerService : ITrainerService
//    {
//        private readonly IUnitOfWork _unit;
//        private readonly IMapper _mapper;

//        public TrainerService(IUnitOfWork unit, IMapper mapper)
//        {
//            _unit = unit;
//            _mapper = mapper;
//        }

//        public async Task<IEnumerable<TrainerReadDto>> GetAllAsync()
//        {
//            var trainers = await _unit.Trainers.GetAllAsync();
//            return _mapper.Map<IEnumerable<TrainerReadDto>>(trainers);
//        }

//        public async Task<TrainerReadDto?> GetByIdAsync(int id)
//        {
//            var trainer = await _unit.Trainers.GetByIdAsync(id);
//            return _mapper.Map<TrainerReadDto>(trainer);
//        }

//        public async Task<TrainerReadDto> CreateAsync(TrainerDto dto)
//        {
//            var trainer = _mapper.Map<Trainer>(dto);

//            await _unit.Trainers.AddAsync(trainer);
//            await _unit.SaveAsync();

//            return _mapper.Map<TrainerReadDto>(trainer);
//        }

//        public async Task<bool> UpdateAsync(int id, TrainerUpdateDto dto)
//        {
//            var trainer = await _unit.Trainers.GetByIdAsync(id);
//            if (trainer == null) return false;

//            _mapper.Map(dto, trainer);
//            _unit.Trainers.Update(trainer);
//            await _unit.SaveAsync();

//            return true;
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var trainer = await _unit.Trainers.GetByIdAsync(id);
//            if (trainer == null) return false;

//            _unit.Trainers.Delete(trainer);
//            await _unit.SaveAsync();

//            return true;
//        }
//    }
//}
