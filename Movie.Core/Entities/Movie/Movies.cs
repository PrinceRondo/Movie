using Movie.Core.Data;
using Movie.Core.Entities.Configuration;
using Movie.Core.Entities.Movie;
using Movie.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Entities.Movie
{
    public class Movies : BaseEntity
    {
        public Movies()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public int CountryId { get; set; }
        public string? Photo { get; set; }


        [ForeignKey("CountryId")]
        public virtual Country? Country { get; set; }
        public virtual ICollection<MovieGenre>? MovieGenres { get; set; }
    }
}
