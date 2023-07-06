using LibrarySystemData.Repositories;
using LibrarySystemModels;

namespace LibraryService
{
    public class SliderService : ISliderService
    {
        public readonly ISliderRepository _sliderRepository;
        
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public List<Slider> GetAll()
        {
            return _sliderRepository.TakeAll().Result;
        }

        public void Save()
        {
            _sliderRepository.SaveData();
        }
       
    }

    public interface ISliderService
    {
        List<Slider> GetAll();
        void Save();
    }
}
