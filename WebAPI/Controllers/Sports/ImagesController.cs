using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebAPI.Models.Sports;
using WebAPI.Models;
using Microsoft.AspNet.OData;

namespace WebAPI.Controllers.Sports
{
    [EnableQuery()]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        private readonly ClubsContext _context;
        public ImagesController(ClubsContext context)
        {
            _context = context ?? throw new Exception();
        }

        [HttpGet]
        public async Task<ICollection<Image>> Get()
        {
            return _context.Images.ToList();
        }
        [HttpPost("upload/{id}")]
        public async Task<IActionResult> Upload(List<IFormFile> files, Guid id)
        {
            var result = new List<Image>();
            try
            {
               
                foreach (var file in files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    _context.Images.Add(new Image() { ImageName = file.FileName, IdTerrain = id });
                    await _context.SaveChangesAsync();
                }
                
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}