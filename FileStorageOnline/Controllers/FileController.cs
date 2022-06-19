using DataAccess.Models;
using Domain;
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
        IFileRepository _fileRepository;
        // private readonly IWebHostEnvironment _appEnvironment;
        public FileController(IFileRepository fileRepository, IWebHostEnvironment appEvironment)
        {
            _fileRepository = fileRepository;
            //_appEnvironment = appEvironment;
        }

        [HttpPost("/LoadMany")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFileCollection files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    // if (memoryStream.Length < 2097152)
                    // {
                    var file = new DbFileModel()
                    {
                        Name = formFile.FileName,
                        FileData = memoryStream.ToArray(),
                        SizeInByte = formFile.Length,
                        Uri = Guid.NewGuid(),
                        FileType = formFile.ContentType

                    };

                    await _fileRepository.Create(file);

                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("File", "The file is too large.");
                    //}
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        [HttpGet("/getFile/{uri}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileModel))]
        public async Task<IActionResult> Get(string uri)
        {
            var file = (await _fileRepository.GetAllAsQuaryable()).FirstOrDefault(x => x.Uri.ToString() == uri);
            if (file == null) return NotFound();

            file.Uri = Guid.NewGuid();
            await _fileRepository.Update(file);
            return File(file.FileData, file.FileType, file.Name);

        }

        [HttpPost("/LoadOne")]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Guid", typeof(Guid))]
        public async Task<IActionResult> FileLoad(IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    var file = new DbFileModel()
                    {
                        Name = formFile.FileName,
                        FileData = memoryStream.ToArray(),
                        SizeInByte = formFile.Length,
                        Uri = Guid.NewGuid(),
                        FileType = formFile.ContentType

                    };

                    await _fileRepository.Create(file);

                }
                else
                {
                    // ModelState.AddModelError("File", "The file is too large.");
                }
            }
            return Ok(new { filename = formFile.FileName, length = formFile.Length });
        }
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileModel))]
        public async Task<IActionResult> Get()
        {
            return Ok((await _fileRepository.GetAllAsQuaryable()).Select(x=> new { name = x.Name, type = x.FileType, uri = $"getFile/{x.Uri}" }));
        }

        [HttpGet("[controller]/file")]
        [SwaggerResponse((int)HttpStatusCode.OK, "File", typeof(VirtualFileResult))]
        public async Task<IActionResult> GetFileAny()
        {
            
            return Ok();
        }
    }
}
