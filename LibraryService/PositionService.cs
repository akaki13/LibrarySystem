using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public List<Position> GetAll()
        {
            return _positionRepository.TakeAll().Result;
        }

        public Position GetById(int id)
        {
            return _positionRepository.FindById(id).Result;   
        }

        public void Add(Position person)
        {
            _positionRepository.AddData(person);
        }

        public void Save()
        {
            _positionRepository.SaveData();
        }

        public void Delete(Position position)
        {
            _positionRepository.DeleteData(position);
        }

        public void Update(Position position)
        {
            _positionRepository.UpdateData(position);
        }
    }
    public interface IPositionService 
    {
        List<Position> GetAll();
        void Save();
        void Add(Position person);
        void Delete(Position position);
        Position GetById(int id);
        void Update(Position position);
    }
}
