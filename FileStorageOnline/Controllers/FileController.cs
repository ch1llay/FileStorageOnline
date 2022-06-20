using DataAccess.Models;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FileStorageOnline.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FileController : Controller
    {
        private IFileInfoRepository _fileRepository;
        // private readonly IWebHostEnvironment _appEnvironment;
        public FileController(IFileInfoRepository fileRepository, IWebHostEnvironment appEvironment)
        {
            _fileRepository = fileRepository;
            //_appEnvironment = appEvironment;
        }

        [HttpPost("/loadMany")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFileCollection files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    var fileData = new DbFileData
                    {
                        Content = memoryStream.ToArray()
                    };
                    var fileDataId = await _fileRepository.CreateFileData(fileData);
                    var file = new DbFileInfo()
                    {
                        Name = formFile.FileName,
                        SizeInByte = formFile.Length,
                        FileType = formFile.ContentType,
                        FileDataId = fileData.Id

                    };

                    await _fileRepository.CreateFileInfo(file);
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        [HttpGet("/getFile/{uri:Guid}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileInfo))]
        public async Task<IActionResult> Get(Guid uri)
        {
            var link = await _fileRepository.GetLink(uri);
            if (link == null)
            {
                return NotFound();
            }

            var fileInfo = await _fileRepository.GetFileByLink(uri);
            if (fileInfo == null)
            {
                return NotFound();
            }
            var fileData = _fileRepository.GetFileData(fileInfo.Id);
            
            return File(file.FileData, file.FileType, file.Name);

        }

        [HttpPost("/LoadOne")]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Guid", typeof(Guid))]
        public async Task<IActionResult> FileLoad(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);


                var file = new DbFileInfo()
                {
                    Name = formFile.FileName,
                    
                    SizeInByte = formFile.Length,
                    FileType = formFile.ContentType

                };

                await _fileRepository.CreateFileInfo(file);

            }
            return Ok(new { filename = formFile.FileName, length = formFile.Length });
        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileInfo))]
        public async Task<IActionResult> Get()
        {
            return Ok((await _fileRepository.GetAllFileInfo()));
        }

    }
}
