using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.Dtos
{
    public class ActorDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public int YearsActive { get; set; }
        public string Picture { get; set; }
        [MaxLength(100)]
        public string Biography { get; set; }
    }
}
