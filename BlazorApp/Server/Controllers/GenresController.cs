using BlazorApp.Shared.Dtos;
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
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<GenreDto>> Get() 
        {
            return await _context.Genres
                .Select(g => new GenreDto 
                { 
                    Id = g.Id, 
                    Name = g.Name
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<GenreDto> Get(int id)
        {
            return await _context.Genres
                .Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GenreDto model) 
        {
            try
            {
                var genre = new Genre(model.Name);

                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(GenreDto model) 
        {
            try
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == model.Id);

                if (genre is null)
                    throw new Exception("Element not found");

                genre.Name = model.Name;

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
