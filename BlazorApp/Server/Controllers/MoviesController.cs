using BlazorApp.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<List<MovieDto>> Get()
        {
            return await _context.Movies
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    Poster = m.Poster,
                    Actors = m.Actors.Select(a => new ActorDto 
                    { 
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Biography = a.Biography,
                        DateOfBirth = a.DateOfBirth,
                        Picture = a.Picture,
                        YearsActive = a.YearsActive
                    }).ToList(),
                    Genres = m.Genres.Select(g => new GenreDto 
                    { 
                        Id = g.Id,
                        Name = g.Name
                    }).ToList()
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<MovieDto> Get(int id)
        {
            return await _context.Movies
                .Select(m => new MovieDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReleaseDate = m.ReleaseDate,
                    Poster = m.Poster,
                    Actors = m.Actors.Select(a => new ActorDto
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Biography = a.Biography,
                        DateOfBirth = a.DateOfBirth,
                        Picture = a.Picture,
                        YearsActive = a.YearsActive
                    }).ToList(),
                    Genres = m.Genres.Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList()
                }).FirstOrDefaultAsync(m => m.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MovieDto model)
        {
            try
            {
                var movie = new Movie 
                {
                    Title = model.Title,
                    Poster = model.Poster,
                    ReleaseDate = (DateTime)model.ReleaseDate,
                    Actors = new List<Actor>(),
                    Genres = new List<Genre>()
                };

                foreach (var actor in model.Actors)
                {
                    var actorDb = _context.Actors.FirstOrDefault(a => a.Id == actor.Id);

                    if (actorDb is not null)
                        movie.Actors.Add(actorDb);
                }

                foreach (var genre in model.Genres)
                {
                    var genreDb = _context.Genres.FirstOrDefault(g => g.Id == genre.Id);

                    if (genreDb is not null)
                        movie.Genres.Add(genreDb);
                }

                await _context.Movies.AddAsync(movie);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(MovieDto model)
        {
            try
            {
                var movie = await _context.Movies
                    .Include(m => m.Actors)
                    .Include(m => m.Genres)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (movie is null)
                    throw new Exception("Element not found");

                List<Actor> newActors = new();
                List<Genre> newGenres = new();

                foreach (var actor in model.Actors)
                {
                    var actorDb = _context.Actors.FirstOrDefault(a => a.Id == actor.Id);

                    if (actorDb is not null)
                        newActors.Add(actorDb);
                }

                foreach (var genre in model.Genres)
                {
                    var genreDb = _context.Genres.FirstOrDefault(g => g.Id == genre.Id);

                    if (genreDb is not null)
                        newGenres.Add(genreDb);
                }

                movie.Title = model.Title;
                movie.Poster = model.Poster;
                movie.ReleaseDate = (DateTime)model.ReleaseDate;
                movie.Actors = newActors;
                movie.Genres = newGenres;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
