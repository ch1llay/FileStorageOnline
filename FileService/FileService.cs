using Domain.Interfaces;
using FileService.Models;

namespace FileService
{
    public class FileService : IFileService
    {
        private readonly IFileInfoRepository _fileInfoRepository;
        private readonly IFileDataRepository _fileDataRepository;
        private readonly ILinkRepository _linkRepository;


        public FileService(IFileInfoRepository fileInfoRepository, IFileDataRepository fileDataRepository, ILinkRepository linkRepository)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileDataRepository = fileDataRepository;
            _linkRepository = linkRepository;
        }
        public async Task<List<FileFullModel>> GetAllFileInfo()
        {

            var files = await _fileInfoRepository.GetAll();
            List<FileInfoModel> result = new List<FileInfoModel>();
            foreach(var file in files)
            {
                var link = await GetLinkForFile(file.Id);
                result.Add(new FileInfoModel
                {
                    Name = file.Name,
                    f
                }
            }
        }
        public async Task<string> GetLinkForFile(Guid fileId)
        {
            return (await _linkRepository.GetLinkIdByFileInfoId(fileId)).ToString("N");
        }
        public Task<FileFullModel> GetFileByLink(string link)
        {
            throw new NotImplementedException();
        }


        public Task<FileInfoModel> GetFileInfoByLink(string link)
        {
            Guid guid;
            if (!Guid.TryParse(link, out guid))
            {
                return null;
            }

            return null;
            // var link = _linkRepository.Get();
        }
    }
}