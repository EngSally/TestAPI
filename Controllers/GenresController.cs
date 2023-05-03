using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Core.DTO;
using TestAPI.Core.Model;
using TestAPI.Data;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
      private readonly ApplicationDbContext _context;
        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllAsync()
        {
          var gene= await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return Ok(gene);
        }

        [HttpPost]
        public async Task<IActionResult> ActionAsync(GenreDTO genreDTO)
        {
            var genre=new Genre{ Name=genreDTO.Name};
           await _context.Genres.AddAsync(genre); 
            _context.SaveChanges();
            return Ok(genre);   

        }



    }
}
