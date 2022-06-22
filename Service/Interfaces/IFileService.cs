using DataAccess.Models;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFileService
    {
        public Task<List<FileInfoServiceModel>> GetAllFilesInfo();
        public Task<FileFullModel> GetFileFullModelByLink(string link);
        public Task<Guid> LoadFile(FileFullModel fileFullModel);
        public Task<string> GetUrlByFileId(Guid id);
        public Task<List<FileInfoServiceModel>> GetFileInfoServicesInTimeLoading(List<FileInfoServiceModel> files);
        public Task<FileInfoServiceModel> Get(Guid id);
    }
}
