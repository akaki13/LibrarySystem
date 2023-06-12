using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class LanguageService : ILanguageService
    {
        public readonly ILanguageRepository _languageRepository;
        
        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public List<Language> GetAll()
        {
            return _languageRepository.TakeAll().Result;
        }

        public void Add(Language language)
        {
            _languageRepository.AddData(language);
        }

        public void Save()
        {
            _languageRepository.SaveData();
        }

        public Language GetById(int id)
        {
            return _languageRepository.FindById(id).Result;
        }

        public void Delete(Language language)
        {
            _languageRepository.DeleteData(language);
        }

        public void Update(Language language)
        {
            _languageRepository.UpdateData(language);
        }
    }

    public interface ILanguageService
    {
        List<Language> GetAll();
        void Add(Language language);
        void Update(Language language);
        void Save();
        Language GetById(int id);
        void Delete(Language language);
    }
}
