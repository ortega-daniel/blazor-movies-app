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
                    Actors = model.Actors.Select(a => new Actor 
                    { 
                        Id = a.Id
                    }).ToList(),
                    Genres = model.Genres.Select(g => new Genre 
                    { 
                        Id = g.Id
                    }).ToList()
                };

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
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == model.Id);

                if (movie is null)
                    throw new Exception("Element not found");

                movie.Title = model.Title;
                movie.Poster = model.Poster;
                movie.ReleaseDate = (DateTime)model.ReleaseDate;
                movie.Actors = model.Actors.Select(a => new Actor { Id = a.Id }).ToList();
                movie.Genres= model.Genres.Select(g => new Genre { Id = g.Id }).ToList();

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
