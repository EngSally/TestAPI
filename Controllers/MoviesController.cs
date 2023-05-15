using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Core.Core.DTO;
using RepositoryPattern.Core.Core.Model;
using RepositoryPattern.Core.Interfaces;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Movie> _moviesRepository;
        private readonly string[] allowExtion=new []{ ".jpg",".png"};
        private readonly int maxSize=3097152;
        public MoviesController(IBaseRepository<Movie> moviesRepository, IMapper mapper)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var allMovie=   _moviesRepository.GetAllAsync();
            var allMovieDto=_mapper.Map<IEnumerable<MovieDetailsDto>>(allMovie);

            return Ok(allMovieDto);
        }





        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {


            var movie= await   _moviesRepository.Find(m=>m.Id==id,new []{"Genre" });
            var moviedto=_mapper.Map<MovieDetailsDto>(movie);
               if (movie is null)
            {
                return BadRequest($"No Movie With id={id}");
            }

            return Ok(movie);
        }

        //[HttpGet("GetByGenreId")]
        //public async Task<IActionResult> GetByGenreId(byte genreId)
        //{
        //    var genre=await  IBaseRepository<Genre>.
        //    if (genre is null) return BadRequest($"No Genra With Id={genreId}");
        //    var movie=await _context.Movies
        //        .Where(m=>m.GenreId==genreId)
        //        .Select(m=> new MovieDetailsDto
        //        {
        //            Id=m.Id,
        //            Rate=m.Rate,
        //            StoryLine=m.StoryLine,
        //            Title=m.Title,
        //            Year=m.Year,
        //            Poster = m.Poster,
        //            GenreId=genre.Id,
        //            GenreName=genre.Name
        //        })
        //        .ToListAsync();
        //    return Ok(movie);
        //}



        [HttpPost]

        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {


            if (dto.Poster is null)
                return BadRequest("Poster Is Required");
            //if (!await _context.Genres.AnyAsync(g => g.Id == dto.GenreId))
            //    return BadRequest($"No Genre With Id={dto.GenreId}");
            if (dto.Poster.Length > maxSize)
                return BadRequest("MaxSize of  Poster Is 2MB");
            if (!allowExtion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg,.png  Are Allowd for Poster");
            using var stream=new MemoryStream();
            await dto.Poster.CopyToAsync(stream);
            var movie=_mapper.Map<Movie>(dto);
            movie.Poster = stream.ToArray();
          
          await  _moviesRepository.Add(movie);
            return Ok(movie);

        }

        //[HttpPut("Id")]
        //public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        //{
        //    var Movie=await _context.Movies.FindAsync(id);
        //        if (Movie is null) return BadRequest($"No Movie With Id={id}");
        //    if (!await _context.Genres.AnyAsync(g => g.Id == dto.GenreId))
        //        return BadRequest($"No Genre With Id={dto.GenreId}");
        //    if(dto.Poster is not  null)
        //    {
        //        if (dto.Poster.Length > maxSize)
        //            return BadRequest("MaxSize of  Poster Is 2MB");
        //        if (!allowExtion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
        //            return BadRequest("Only .jpg,.png  Are Allowd for Poster");
        //        using var stream=new MemoryStream();
        //        await dto.Poster.CopyToAsync(stream);
        //        Movie.Poster = stream.ToArray();
        //    }
        //    Movie.Year = dto.Year;
        //    Movie.StoryLine = dto.StoryLine;
        //    Movie.Rate = dto.Rate;
        //    Movie.Title = dto.Title;
        //    Movie.GenreId= dto.GenreId;
        //    _context.SaveChanges();
        //    return Ok(Movie);   
        //}

        //[HttpDelete("Id")]
        //public  async Task<IActionResult>DeleteAsync(int id)
        //{
        //    var movie=await _context.Movies.FindAsync(id);
        //    if (movie == null) return BadRequest($"No Movie With Id={id}");
        //     _context.Movies.Remove(movie);
        //    _context.SaveChanges();
        //    return Ok();
        //}


    }
}
