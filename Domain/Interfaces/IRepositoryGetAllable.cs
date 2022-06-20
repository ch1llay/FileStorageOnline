namespace Domain.Interfaces
{
    public interface IRepositoryGetAllable<T>
    {
        public Task<IEnumerable<T>> GetAll();
    }
}
