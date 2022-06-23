using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Interfaces
{
    public interface IFileService
    {
        public Task<List<FileInfoServiceModel>> GetAllFilesInfo();
        public Task<FileFullModel> GetFileFullModelByLink(string link);
        public Task<Guid> LoadFile(FileFullModel fileFullModel);
        public Task<string> GetUriByFileId(Guid id);
        public Task<List<FileInfoServiceModel>> GetFileInfoServicesInTimeLoading(List<FileInfoServiceModel> files);
        public Task<FileInfoServiceModel> Get(Guid id);
    }
}