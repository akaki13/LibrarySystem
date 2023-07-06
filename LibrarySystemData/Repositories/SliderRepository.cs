using LibrarySystemData.Infrastructure;
using LibrarySystemModels;


namespace LibrarySystemData.Repositories
{
    public class SliderRepository : RepositoryBase<Slider>, ISliderRepository
    {
        public SliderRepository(LibraryContext context)
           : base(context) { }
        

    }

    public interface ISliderRepository : IRepositoryBase<Slider>
    {
    }
}
