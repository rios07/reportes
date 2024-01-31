using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace track3_api_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public LogController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpGet("Download/{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            string path = "";
            string result = "";
            int a = 1;
            foreach (var item in filename)
            {
                if (int.TryParse(Convert.ToString(item), out a))
                { result = result + item; }
            }

            if (filename == null)
                return Content($"no existe el fichero {filename}");

            if (_environment.IsDevelopment())
            {
                path = Directory.GetCurrentDirectory() + "\\Logs\\" + result + "\\";
            }
            else
            {
                path = Directory.GetCurrentDirectory() + "//Logs//" + result + "//";
            }
            path = path + filename + ".txt";

            if (!System.IO.File.Exists(path))
            {
                return BadRequest($"No se existe el fichero {filename}");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            List<FileStreamResult> list = new List<FileStreamResult>();
            list.Add(File(memory, "application/octet-stream", Path.GetFileName(path)));

            return File(memory, "application/octet-stream", Path.GetFileName(path));
        }
    }
}
