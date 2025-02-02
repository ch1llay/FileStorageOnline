﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace OnlineFileStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;


        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("/loadMany")]
        public async Task<IActionResult> LoadManyFiles(IFormFileCollection formFiles)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var uploadedFiles = new List<FileInfoServiceModel>();
            foreach (var formFile in formFiles)
            {
                using var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);
                var file = new FileFullModel
                {
                    Name = formFile.FileName,
                    SizeInByte = formFile.Length,
                    FileType = formFile.ContentType,
                    Content = memoryStream.ToArray()
                };
                var id = await _fileService.LoadFile(file);

                var loadingStatus = id == Guid.Empty
                    ? "failed"
                    : "success";

                uploadedFiles.Add(new FileInfoServiceModel
                {
                    Id = id,
                    Name = formFile.FileName,
                    FileType = formFile.ContentType,
                    SizeInByte = formFile.Length,
                    Status = loadingStatus
                });
            }

            return Ok(uploadedFiles);
        }

        [HttpPost("/loadOne")]
        public async Task<IActionResult> FileLoad(IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            var file = new FileFullModel
            {
                Name = formFile.FileName,
                SizeInByte = formFile.Length,
                FileType = formFile.ContentType,
                Content = memoryStream.ToArray()
            };

            var id = await _fileService.LoadFile(file);
            if (id == Guid.Empty)
            {
                return StatusCode(500);
            }

            return Ok(id);
        }

        [HttpGet("getAllFiles")]
        public async Task<IActionResult> GetAllFiles()
        {
            return Ok(await _fileService.GetAllFilesInfo());
        }

        [HttpGet("/getUrl")]
        public async Task<IActionResult> GetUrlForFile([FromQuery] Guid fileId)
        {
            if (fileId == Guid.Empty)
            {
                return BadRequest();
            }

            var uri = await _fileService.GetUriByFileId(fileId);
            if (uri == null)
            {
                return StatusCode(500);
            }

            return Ok($"https://{Request.Host.Value}/downloadFile/{uri}");
        }

        [HttpGet("/downloadFile/{uri}")]
        public async Task<IActionResult> Download(string uri)
        {
            if (!Guid.TryParse(uri, out var _))
            {
                return BadRequest();
            }

            var file = await _fileService.GetFileFullModelByLink(uri);
            if (file == null)
            {
                return NotFound("Ссылка не существует либо недействительна");
            }

            return File(file.Content, file.FileType, file.Name);
        }
    }
}