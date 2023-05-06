using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Core.DTO;
using TestAPI.Data;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
       private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async  Task<IActionResult> CreateAsync([FromForm]MovieDto dto)
        {


            if(dto.Poster is null)  
                return BadRequest("Poster Is Required");
            if (!await _context.Genres.AnyAsync(g => g.Id == dto.GenreId))
                return BadRequest($"No Genre With Id={dto.GenreId}");
            using var stream=new MemoryStream();
            await  dto.Poster.CopyToAsync(stream);
            var movie=new Movie()
            {
                GenreId=dto.GenreId,
                Title = dto.Title,
                Rate = dto.Rate,
                Year = dto.Year,
                StoryLine = dto.StoryLine,
                Poster=stream.ToArray()

            };
          await  _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return Ok(movie);

        }


    }
}
