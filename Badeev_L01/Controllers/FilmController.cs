using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Badeev_L01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FilmController : ControllerBase
    {
        private readonly DataContext _context;
        public FilmController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("By_prodID")]
        public async Task<ActionResult<List<Film>>> Get(int ProducerId)
        {
            var film = await _context.Films
                .Where(c => c.ProducerId == ProducerId)
                .ToListAsync();
            return film;
        }

        [HttpGet("By_prod_name/{name}")]
        public async Task<ActionResult<List<Film>>> Get(string name)
        {
            var prodname = _context.Producers
                .Where(a => a.Name == name)
                .Select(a => a.Id).ToList();
            var film = await _context.Films
                .Where(c => c.ProducerId == prodname[0])
                .ToListAsync();
            if (film == null)
                return NotFound();
            return film;
        }

        //[HttpGet("By_name/{name}")]
        //public async Task<ActionResult<Film>> Get(string name)
        //{
        //    var film = await _context.Films
        //        .Where(c => c.Name == name)
        //        .ToListAsync();
        //    if (film == null)
        //        return BadRequest("Film is not found");
        //    return Ok(film);
        //}

        [HttpGet("More_or_equal_rating/{rating}")]
        public async Task<ActionResult<Film>> Get(double rating)
        {
            var films = _context.Films
                .Where(a => a.Rating >= rating)
                .Select(a => new { a.Name, a.Rating, a.Description }).ToList();

            string Result = "";
            foreach (var f in films)
                Result = Result + f.ToString() + "\n";
    
            return Ok(Result);
        }

        [HttpPost("{producerId}")]
        public async Task<ActionResult<List<Film>>> AddFilm(int producerId, CreateFilmDto film)
        {
            var prod = await _context.Producers.Include(a => a.Films).FirstOrDefaultAsync(m => m.Id == producerId);
            //var prod = await _context.Producers.FindAsync(film.ProducerId);
            if (prod == null)
                return NotFound();

            var newFilm = new Film
            {
                Name = film.Name,
                Genre = film.Genre,
                Rating = film.Rating,
                Description = film.Description,
                ProducerId = prod.Id,
                Producer = prod
            };

            _context.Films.Add(newFilm);
            await _context.SaveChangesAsync();

            return await Get(prod.Id);
            //return Ok(await _context.Films.ToListAsync());
        }


        [HttpPut]
        public async Task<ActionResult<List<Film>>> UpdateFilm(Film request)
        {
            var dbFilm = await _context.Films.FindAsync(request.Id);
            if (dbFilm == null)
                return BadRequest("Film is not found");
            
            dbFilm.Name = request.Name;
            dbFilm.Genre = request.Genre;
            dbFilm.Rating = request.Rating;
            dbFilm.Description = request.Description;

            await _context.SaveChangesAsync();
            return Ok(await _context.Films.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Film>>> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);

            if (film == null)
                return BadRequest("Film is not found");
            
            _context.Remove(film);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.Films.ToListAsync());
        }
    }
}
