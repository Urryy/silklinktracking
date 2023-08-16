using cargosiklink.DataContext;
using cargosiklink.Models;
using cargosiklink.Repository.Interfaces;

namespace cargosiklink.Repository
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDataContext _context;
        public UserRepository(ApplicationDataContext context)
        {
            _context = context;
        }
        public async Task Create(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public async Task Update(User entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
