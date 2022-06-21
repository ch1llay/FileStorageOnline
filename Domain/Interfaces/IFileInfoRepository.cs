using DataAccess.Models;

namespace Domain.Interfaces
{
    public interface IFileInfoRepository : IRepository<DbFileInfo>
    {
        public Task<List<FileModel>> GetFileModels();
    }
}
