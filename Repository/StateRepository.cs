using cargosiklink.DataContext;
using cargosiklink.Models;
using cargosiklink.Repository.Interfaces;

namespace cargosiklink.Repository
{
    public class StateRepository : IBaseRepository<State>
    {
        private readonly ApplicationDataContext _dbContext;
        public StateRepository(ApplicationDataContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task Create(State entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(State entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<State> GetAll()
        {
            return _dbContext.States;
        }

        public async Task Update(State entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
