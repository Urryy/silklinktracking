using cargosiklink.DataContext;
using cargosiklink.Models;
using cargosiklink.Repository.Interfaces;

namespace cargosiklink.Repository
{
    public class NumberTrackRepository : IBaseRepository<NumberTrack>
    {
        private readonly ApplicationDataContext _dbContext;
        public NumberTrackRepository(ApplicationDataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(NumberTrack entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(NumberTrack entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<NumberTrack> GetAll()
        {
            return _dbContext.NumberTracks;
        }

        public async Task Update(NumberTrack entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
