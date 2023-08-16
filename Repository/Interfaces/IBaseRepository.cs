namespace cargosiklink.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        IEnumerable<T> GetAll();
        Task Delete(T entity);
        Task Update(T entity);
    }
}
