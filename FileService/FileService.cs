using DataAccess.Models;
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
        public async Task<List<FileModel>> GetAllFilesInfo()
        {

            return await _fileInfoRepository.GetFileModels();

        }

        public async Task<FileFullModel> GetFileFullModelByLink(string link)
        {
            Guid linkId;
            if (!Guid.TryParse(link, out linkId))
            {
                return null;
            }
            var linkModel = await _linkRepository.Get(linkId);
            if (linkModel == null)
            {
                return null;
            }
            var fileInfo = await _fileInfoRepository.Get(linkModel.FileInfoId);
            if (fileInfo == null)
            {
                return null;
            }
            var fileData = await _fileDataRepository.Get(fileInfo.FileDataId);
            if (fileData == null)
            {
                return null;
            }
            return new FileFullModel
            {
                Name = fileInfo.Name,
                FileType = fileInfo.FileType,
                SizeInByte = fileInfo.SizeInByte,
                Content = fileData.Content
            };
        }
        public async Task<string> GetLinkForFile(Guid fileId)
        {
            return (await _linkRepository.GetLinkIdByFileInfoId(fileId)).ToString("N");
        }
    }
}