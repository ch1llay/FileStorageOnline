namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task<Guid> Create(T entity);
        public Task<T?> Update(T entity);
        public Task<bool> Delete(Guid id);
        public Task<T?> Get(Guid id);
    }
}
