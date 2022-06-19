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
        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
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
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "DbFileModel", typeof(DbFileModel))]
        public async Task<IActionResult> FileGet(Guid id)
        {
            return Ok(await _fileRepository.Get(id));
        }
    }
}
