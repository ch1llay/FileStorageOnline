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
        Task<Guid> CreateLink(DbOneTimeLinkModel model);
        Task<bool> DeleteLink(Guid id);
        Task<DbFileInfo> GetFileByLink(Guid linkId);
        Task<DbOneTimeLinkModel> GetLink(Guid id);
        Task<IEnumerable<DbOneTimeLinkModel>> GetAllLink();

        Task<DbFileData> GetFileData(Guid fileInfoId);





    }
}
