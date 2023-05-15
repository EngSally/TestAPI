using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Core.Interfaces;

using RepositoryPattern.Core.Core.Model;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
      private readonly IBaseRepository<Genre> _genrRepository;
        public GenresController(IBaseRepository<Genre> genrRepository)
        {
            _genrRepository = genrRepository;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllAsync()
        {
            var gene= await _genrRepository.GetAllAsync();
            return Ok(gene);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateAsync([FromBody]string  name)
        //{
           
        //    var genre=new Genre{ Name=name};
        //    if(await CheckNameFound(genre))
        //    {
        //        return BadRequest("Name Found Before");
        //    }
        //   await _context.Genres.AddAsync(genre); 
        //    _context.SaveChanges();
        //    return Ok(genre);   

        //}

        //[HttpPut("{id}")]
        //public  async Task<IActionResult> UpdateAsync(int id, [FromBody] Genre ge)
        //{
        //    if(id != ge.Id) { return BadRequest("Id Not Simpalre Header And Body"); }
        //    var genre=  await _context.Genres.FirstOrDefaultAsync(g=>g.Id==ge.Id);
        //    if(genre is null) { return NotFound($"No Genre with id={id}"); }
        //    genre.Name = ge.Name;
        //    if ( await CheckNameFound(genre))
        //    {
        //        return BadRequest("Name Found Before");
        //    }
        //    _context.SaveChanges();
        //    return Ok();
        //}

        //[HttpDelete("Id")]
        //public  async Task<IActionResult> DeleteAsync(int id)
        //{
        //    var genre=  await _context.Genres.FirstOrDefaultAsync(g=>g.Id==id);
        //    if (genre is null) { return NotFound($"No Genre with id={id}"); }
        //    _context.Genres.Remove(genre);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        //private async Task  <bool> CheckNameFound(Genre genre)
        //{

        //    if (genre.Id == 0) { return await  _context.Genres.AnyAsync(g => g.Name.Equals(genre.Name)); }//Create
        //    return  await  _context.Genres.AnyAsync(g => g.Name.Equals(genre.Name) && g.Id!= genre.Id);//update
        //}



    }
}
