using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesLab2.Models;

namespace MoviesLab2.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDbContext _context;

        public MoviesController(MoviesDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        /// <summary>
        /// Gets a list of all movies;
        /// </summary>
        /// <param name="from">Filter for movies added from this date time(inclusive). Leave empty for now lower limit..</param>
        /// <param name="to">Filter for movies added up to this date time(inclusive). Leave empty for now upper limit.</param>
        /// <returns>A list of MOvie objects</returns>
        ///<response code = "201">Returns the list of movies</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(
            [FromQuery]DateTime? from = null,
            [FromQuery]DateTime? to = null)
        {
            IQueryable<Movie> result = _context.Movies;
            if (from != null)
            {
                result = result.Where(f => from <= f.DateAdded);
            }
            if (to != null)
            {
                result = result.Where(f => f.DateAdded <= to);
            }

            var resultList = await result
                .OrderByDescending(m => m.YearOfRelease)
                .ToListAsync();
            return resultList;
        }
        /// <summary>
        /// Gets a specific movie
        /// </summary>
        /// <param name="id">The id of the movie you want to return</param>
        /// <returns>The movie with the id you gave.</returns>
        ///<response code = "201">Returns the movie</response>
        ///<response code = "404">Not found, if the param id does not exist.</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(long id)
        {
            var movie = await _context.Movies
                                             .Include(m => m.Comments)
                                             .FirstOrDefaultAsync (m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <param name="id">Edit the id of the movie you want to update</param>
        /// <param name="movie">Enter the new name of the movie.</param>
        /// <returns>The updated movie.</returns>
        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        /// <summary>
        /// Creates a Movie objects.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///         "title": "First Movie",
        ///         "description": "Movie description",
        ///         "genre": "comedy",
        ///         "minute": 100,
        ///         "year of release": 2000,
        ///         "director": "ion",
        ///         "dateadded":"2000-01-01T00:00:00",
        ///         "rating":1,
        ///         "watched": true
        ///      } 
        /// </remarks>
        ///<returns> a newly created Movie</returns>
        ///<response code = "201"> Returns the newly created movie.</response>
        ///<response code = "400"> If the title is no longer than 2 characters. or
        ///                        If the rating is  not between 1 and 10 inclusive. or
        ///                        If the description's maximumLength is longer than 25characters. or
        ///                        If the description's maximumLength is longer than 25characters. or 
        ///                        If the year of release is not between  1950 and 2020 inclusive. or 
        ///                        If the date added time is greater than current date time). </response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }


        /// <summary>
        /// Delets a specific Movie object.
        /// </summary>
        /// <param name="id">The id of the movie you want to delete.</param>
        /// <response code = "201">When the movie was deleted succesfully</response>
        /// <response code = "400">When the movie was not deleted succesfully</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(long id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
