using DataAccess.Models;
using Domain.Interfaces;
using Service.Models;

namespace Service
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
        public async Task<List<FileInfoService>> GetAllFilesInfo(string webRoot)
        {

            return (await _fileInfoRepository.GetFileModels()).Select(x => new FileInfoService
            {
                Id = x.Id,
                Name = x.Name,
                FileType = x.FileType,
                SizeInByte = x.SizeInByte,
                Link = $"{webRoot}{x.Link}"

            }).ToList();

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

        public async Task<Guid> LoadFile(FileFullModel fileFullModel)
        {
            var fileData = new DbFileData
            {
                Content = fileFullModel.Content
            };
            var fileDataId = await _fileDataRepository.Create(fileData);
            var file = new DbFileInfo()
            {
                Name = fileFullModel.Name,
                SizeInByte = fileFullModel.SizeInByte,
                FileType = fileFullModel.FileType,
                FileDataId = fileData.Id

            };

            var id = await _fileInfoRepository.Create(file);
            return id;
        }
    }
}