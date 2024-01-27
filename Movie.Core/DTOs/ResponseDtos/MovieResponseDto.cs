using Movie.Core.Entities.Configuration;
using Movie.Core.Entities.Movie;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.DTOs.ResponseDtos
{
    public record MovieResponseDto
    {
        public MovieResponseDto()
        {
            MovieGenres = new HashSet<MovieGenre>();
        }
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Photo { get; set; }
        public virtual ICollection<MovieGenre>? MovieGenres { get; set; }
    }
}
