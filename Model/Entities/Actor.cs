using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int YearsActive { get; set; }
        public string Picture { get; set; }
        [MaxLength(100)]
        public string Biography { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
