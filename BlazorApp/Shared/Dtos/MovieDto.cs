using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string Title { get; set; }
        public string Poster { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public DateTime? ReleaseDate { get; set; }
        public List<GenreDto> Genres { get; set; }
        public List<ActorDto> Actors { get; set; }
    }
}
