using LibrarySystemData.Repositories;
using LibrarySystemModels;


namespace LibraryService
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public List<Publisher> GetAll()
        {
            return _publisherRepository.TakeAll().Result;
        }

        public void Add(Publisher publisher)
        {
            _publisherRepository.AddData(publisher);
        }

        public void Save()
        {
            _publisherRepository.SaveData();
        }

        public Publisher GetById(int id)
        {
            return _publisherRepository.FindById(id).Result;
        }

        public void Delete(Publisher publisher)
        {
            _publisherRepository.DeleteData(publisher);
        }

        public void Update(Publisher publisher)
        {
            _publisherRepository.UpdateData(publisher);
        }
    }
    public interface IPublisherService
    {
        List<Publisher> GetAll();
        void Add(Publisher publisher);
        void Update(Publisher publisher);
        void Save();
        Publisher GetById(int id);
        void Delete(Publisher publisher);
    }
}
