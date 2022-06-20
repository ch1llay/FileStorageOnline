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

        public async Task<List<FileInfoModel>> GetAllFileInfo()
        {
            Guid.
            return (await _fileInfoRepository.GetAll()).Select(x => new FileInfoModel
            {
                Name = x.Name,
                FileType = x.FileType,
                SizeInByte = x.SizeInByte,
                Lin
            })
        }

        public Task<FileModel> GetFileByLink(string link)
        {
            throw new NotImplementedException();
        }

        public Task<FileInfoModel> GetFileInfoByLink(string link)
        {
            var link = _linkRepository.Get();
        }
    }
}