using DataAccess.Models;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFileService
    {
        public Task<List<FileModelService>> GetAllFilesInfo(string webRoot);
        public Task<FileFullModel> GetFileFullModelByLink(string link);
        public Task<Guid> LoadFile(FileFullModel fileFullModel);
    }
}
