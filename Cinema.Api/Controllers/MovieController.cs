using Cinema.Application.DTOs;
using Cinema.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Api.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        [Authorize]
        public class MoviesController : ControllerBase
        {
            private readonly IMovieService _movieService;
            private readonly ILogger<MoviesController> _logger;

            public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
            {
                _movieService = movieService;
                _logger = logger;
            }

            [HttpGet]
            [AllowAnonymous]
            public async Task<ActionResult<IEnumerable<MovieDto>>> GetAll()
            {
                try
                {
                    var movies = await _movieService.GetAllMoviesAsync();
                    return Ok(movies);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting movies");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpGet("{id}")]
            [AllowAnonymous]
            public async Task<ActionResult<MovieDto>> GetById(string id)
            {
                try
                {
                    var movie = await _movieService.GetMovieByIdAsync(id);
                    if (movie == null)
                        return NotFound($"Movie with id {id} not found");
                    return Ok(movie);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting movie {Id}", id);
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpGet("search")]
            [AllowAnonymous]
            public async Task<ActionResult<IEnumerable<MovieDto>>> Search([FromQuery] string term)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(term))
                        return BadRequest("Search term cannot be empty");

                    var movies = await _movieService.SearchMoviesAsync(term);
                    return Ok(movies);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error searching movies");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpGet("genre/{genre}")]
            [AllowAnonymous]
            public async Task<ActionResult<IEnumerable<MovieDto>>> GetByGenre(string genre)
            {
                try
                {
                    var movies = await _movieService.GetMoviesByGenreAsync(genre);
                    return Ok(movies);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting movies by genre {Genre}", genre);
                    return StatusCode(500, "Internal server error");
                }
            }

           

            [HttpGet("top-rated")]
            [AllowAnonymous]
            public async Task<ActionResult<IEnumerable<MovieDto>>> GetTopRated([FromQuery] int count = 10)
            {
                try
                {
                    var movies = await _movieService.GetTopRatedAsync(count);
                    return Ok(movies);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting top rated movies");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpPost]
            [Authorize(Roles = "Admin")]
            public async Task<ActionResult<MovieDto>> Create(CreateMovieDto createDto)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    var movie = await _movieService.CreateMovieAsync(createDto);
                    return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating movie");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpPut("{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Update(string id, UpdateMovieDto updateDto)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return BadRequest(ModelState);

                    await _movieService.UpdateMovieAsync(id, updateDto);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating movie", id);
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<IActionResult> Delete(string id)
            {
                try
                {
                    await _movieService.DeleteMovieAsync(id);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting movie", id);
                    return StatusCode(500, "Internal server error");
                }
            }
        }
    }

