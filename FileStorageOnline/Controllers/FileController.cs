using DataAccess.Models;
using Domain.Interfaces;
using Service;
using Service.Models;
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
        private IFileService _fileService;
        private readonly IWebHostEnvironment _appEnvironment;
        public FileController(IFileInfoRepository fileRepository, IFileService fileService, IWebHostEnvironment appEvironment)
        {
            _fileRepository = fileRepository;
            _fileService = fileService;
            _appEnvironment = appEvironment;
            
        }

        //[HttpPost("/loadMany")]
        //public async Task<IActionResult> OnPostUploadAsync(IFormFileCollection files)
        //{
        //    long size = files.Sum(f => f.Length);

        //    foreach (var formFile in files)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {

        //            var file = new FileFullModel
        //            {
        //                Name = formFile.FileName,
        //                SizeInByte = formFile.Length,
        //                FileType = formFile.ContentType,
        //                Content = memoryStream.ToArray()
        //            };
        //            _fileService.LoadFile(file);
        //        }
                
        //    }

        //    // Process uploaded files
        //    // Don't rely on or trust the FileName property without validation.

        //    return Ok(new { count = files.Count, size });
        //}

        [HttpGet("/downloadFile/{uri}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileInfo))]
        public async Task<IActionResult> Get(string uri)
        {
            if (!Guid.TryParse(uri, out Guid _))
            {
                return BadRequest();
            }
            var file = await _fileService.GetFileFullModelByLink(uri);
            if (file == null)
            {
                return NotFound();
            }

            return File(file.Content, file.FileType, file.Name);

        }

        [HttpPost("/LoadOne")]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Guid", typeof(Guid))]
        public async Task<IActionResult> FileLoad(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {

                var file = new FileFullModel
                {
                    Name = formFile.FileName,
                    SizeInByte = formFile.Length,
                    FileType = formFile.ContentType,
                    Content = memoryStream.ToArray()
                };
                var id = await _fileService.LoadFile(file);
                return Ok(id);
            };
            
        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileInfo))]
        public async Task<IActionResult> Get()
        {
            return Ok((await _fileService.GetAllFilesInfo(_appEnvironment.WebRootPath)));
        }

    }
}
