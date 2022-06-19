using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IFileRepository : IRepository<DbFileInfo>
    {
        Task<Guid> CreateFileInfo(DbFileInfo model);
        Task<DbFileInfo?> UpdateFileInfo(DbFileInfo model);
        Task<bool> DeleteFileInfo(Guid id);
        Task<IEnumerable<DbFileInfo>> GetAllFileInfo();
        Task<DbFileInfo?> GetFileInfo(Guid id);

        Task<Guid> CreateLink(DbOneTimeLinkModel model);
        Task<bool> DeleteLink(Guid id);
        Task<DbFileData> GetFileByLink(Guid linkId);
        Task<DbOneTimeLinkModel> GetLink(Guid id);

        Task<Guid> CreateFileData(DbFileData model);
        Task<DbFileData> GetFileData(Guid id);






    }
}
