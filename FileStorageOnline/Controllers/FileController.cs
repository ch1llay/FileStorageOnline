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
        private readonly IWebHostEnvironment _appEnvironment;
        public FileController(IFileRepository fileRepository, IWebHostEnvironment appEvironment)
        {
            _fileRepository = fileRepository;
            _appEnvironment = appEvironment;
        }
        
        [HttpPost]
        //[SwaggerResponse((int)HttpStatusCode.OK, "Guid", typeof(Guid))]
        public async Task<IActionResult> FileLoad(string filename)
        {
            return Ok(await _fileRepository.Create(new DbFileModel
            {
                Name = filename,
                Path = filename,
            }));
        }
        //[HttpGet]
        //[SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileModel))]
        //public async Task<IActionResult> FileGet(Guid id)
        //{
        //    return Ok(await _fileRepository.Get(id));
        //}

        [HttpGet("[controller]/file")]
        [SwaggerResponse((int)HttpStatusCode.OK, "File", typeof(VirtualFileResult))]
        public async Task<IActionResult> GetFileAny()
        {
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "Files/TextFile.txt");
            // Тип файла - content-type
            string file_type = "file/txt";
            // Имя файла - необязательно
            string file_name = "file.txt";
            return PhysicalFile(file_path, file_type, file_name);
        }
    }
}
