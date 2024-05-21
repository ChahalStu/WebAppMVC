using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api")]
    public class FileController:ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadPatientFile([FromForm] FileModel patientFile)
        {
            try
            {
                await _fileService.Upload(patientFile);
                return Ok("Patient file saved successfully");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
 
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetPatientFile(string name)
        {
            try
            {
                var PatientFileStream = await _fileService.Get(name);
                return File(PatientFileStream, "application/octet-stream", name);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            //return File(PatientFileStream,"application/pdf",name);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeletePatientFile(string name)
        {
            try
            {
                await _fileService.Delete(name);
                return Ok("Patient file deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetListOfPatientFiles()
        {
            try
            {
                var fileList = await _fileService.List();
                return Ok(fileList);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadPatientFile(string name, string filePath)
        {
            try
            {
                await _fileService.DownloadToFile(name, filePath);
                return Ok("File downloaded successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdatePatientFile([FromQuery] string name, [FromBody] Stream newContent)
        {
            try
            {
                await _fileService.Update(name, newContent);
                return Ok("File updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("metadata")]
        public async Task<IActionResult> GetPatientFileMetadata([FromQuery] string name)
        {
            try
            {
                var metadata = await _fileService.GetMetadata(name);
                return Ok(metadata);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
