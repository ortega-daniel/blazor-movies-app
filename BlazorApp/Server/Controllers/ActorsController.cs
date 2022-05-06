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
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context) 
            => _context = context;

        [HttpGet]
        public async Task<List<ActorDto>> Get()
        {
            return await _context.Actors
                .Select(a => new ActorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    DateOfBirth = a.DateOfBirth,
                    YearsActive = a.YearsActive,
                    Picture = a.Picture,
                    Biography = a.Biography
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActorDto> Get(int id)
        {
            return await _context.Actors
                .Select(a => new ActorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    DateOfBirth = a.DateOfBirth,
                    YearsActive = a.YearsActive,
                    Picture = a.Picture,
                    Biography = a.Biography
                }).FirstOrDefaultAsync(a => a.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ActorDto model)
        {
            try
            {
                var actor = new Actor 
                { 
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = (DateTime)model.DateOfBirth,
                    YearsActive= model.YearsActive,
                    Picture = model.Picture,
                    Biography = model.Biography
                };

                await _context.Actors.AddAsync(actor);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(ActorDto model)
        {
            try
            {
                var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == model.Id);

                if (actor is null)
                    throw new Exception("Element not found");

                actor.FirstName = model.FirstName;
                actor.LastName = model.LastName;
                actor.DateOfBirth = (DateTime)model.DateOfBirth;
                actor.YearsActive = model.YearsActive;
                actor.Picture = model.Picture;
                actor.Biography = model.Biography;

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
