using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Domain.Interfaces;
using Service.Interfaces;
using Service.Models;

namespace Service
{
    public class FileService : IFileService
    {
        private readonly IFileDataRepository _fileDataRepository;
        private readonly IFileInfoRepository _fileInfoRepository;
        private readonly ILinkRepository _linkRepository;

        public FileService(IFileInfoRepository fileInfoRepository, IFileDataRepository fileDataRepository,
            ILinkRepository linkRepository)
        {
            _fileInfoRepository = fileInfoRepository;
            _fileDataRepository = fileDataRepository;
            _linkRepository = linkRepository;
        }

        public async Task<FileInfoServiceModel> Get(Guid id)
        {
            var file = await _fileInfoRepository.Get(id);
            if (file == null)
            {
                return null;
            }

            return new FileInfoServiceModel
            {
                Id = file.Id,
                Name = file.Name,
                SizeInByte = file.SizeInByte,
                FileType = file.FileType
            };
        }

        public async Task<List<FileInfoServiceModel>> GetAllFilesInfo()
        {
            return (await _fileInfoRepository.GetAll()).Select(x => new FileInfoServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                FileType = x.FileType,
                SizeInByte = x.SizeInByte
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

            if ((DateTime.Now - linkModel.UploadDate).TotalMinutes > 15)
            {
                return null;
            }

            var fileInfo = await _fileInfoRepository.Get(linkModel.FileInfoId);
            if (fileInfo == null)
            {
                return null;
            }

            var fileData = await _fileDataRepository.GetByFileInfoId(fileInfo.Id);
            if (fileData == null)
            {
                return null;
            }

            await _linkRepository.Delete(linkId);
            return new FileFullModel
            {
                Name = fileInfo.Name,
                FileType = fileInfo.FileType,
                SizeInByte = fileInfo.SizeInByte,
                Content = fileData.Content
            };
        }

        public async Task<List<FileInfoServiceModel>> GetFileInfoServicesInTimeLoading(List<FileInfoServiceModel> files)
        {
            var filesWithIds = new List<FileInfoServiceModel>();
            foreach (var file in files)
            {
                var fileId = await _fileInfoRepository.Create(new DbFileInfo
                {
                    Name = file.Name,
                    FileType = file.FileType,
                    SizeInByte = file.SizeInByte
                });
                if (fileId == null)
                {
                    file.Id = Guid.Empty;
                }

                file.Id = fileId;
                filesWithIds.Add(file);
            }

            return filesWithIds;
        }

        public async Task<string> GetUriByFileId(Guid id)
        {
            var link = await _linkRepository.Create(
                new DbLink
                {
                    UploadDate = DateTime.Now,
                    FileInfoId = id
                });
            if (link == Guid.Empty)
            {
                return null;
            }

            return link.ToString("N");
        }

        public async Task<Guid> LoadFile(FileFullModel fileFullModel)
        {
            var file = new DbFileInfo
            {
                Name = fileFullModel.Name,
                SizeInByte = fileFullModel.SizeInByte,
                FileType = fileFullModel.FileType
            };
            var fileInfoId = await _fileInfoRepository.Create(file);
            if (fileInfoId == Guid.Empty)
            {
                return Guid.Empty;
            }

            var fileData = new DbFileData
            {
                FileInfoId = fileInfoId,
                Content = fileFullModel.Content
            };
            var fileDataBd = await _fileDataRepository.Create(fileData);

            if (fileDataBd == Guid.Empty)
            {
                return Guid.Empty;
            }


            return fileInfoId;
        }
    }
}