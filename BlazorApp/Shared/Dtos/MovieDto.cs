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
        public string TitleFit
        {
            get
            {
                if (string.IsNullOrEmpty(Title))
                {
                    return null;
                }

                if (Title.Length > 60)
                {
                    return Title.Substring(0, 57) + "...";
                }
                else
                {
                    return Title;
                }
            }
        }
        [MinLength(1)]
        public List<GenreDto> Genres { get; set; } = new();
        [MinLength(1)]
        public List<ActorDto> Actors { get; set; } = new();
    }
}
