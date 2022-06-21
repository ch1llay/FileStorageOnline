using FileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService
{
    public interface IFileService
    {
        public Task<List<FileInfoModel>> GetAllFileInfo();
        public Task<FileInfoModel> GetFileInfoByLink(string link);
        public Task<FileFullModel> GetFileByLink(string link);
    }
}
