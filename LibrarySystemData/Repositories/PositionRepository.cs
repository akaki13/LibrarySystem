using LibrarySystemData.Infrastructure;
using LibrarySystemModels;

namespace LibrarySystemData.Repositories
{
    public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(LibraryContext context)
            : base(context) { }
    }
    public interface IPositionRepository : IRepositoryBase<Position>
    {
        
    }
}
