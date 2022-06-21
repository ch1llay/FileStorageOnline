using DataAccess.Models;

namespace Domain.Interfaces
{
    public interface IFileDataRepository
    {
        public Task<Guid> Create(DbFileData fileInfo);
        public Task<DbFileData?> Get(Guid id);
    }
}
