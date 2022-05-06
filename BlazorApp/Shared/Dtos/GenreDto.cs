using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Dtos
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GenreDtoValidator : AbstractValidator<GenreDto> 
    {
        public GenreDtoValidator()
        {
            RuleFor(genre => genre.Name)
                .NotEmpty()
                .MaximumLength(15)
                .WithName("Genre");
        }
    }
}
