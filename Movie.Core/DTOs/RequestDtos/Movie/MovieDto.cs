using Microsoft.AspNetCore.Http;
using Movie.Core.Common.Helpers;
using Movie.Core.Entities.Movie;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.DTOs.RequestDtos.Movie
{
    public record MovieDto
    {
        [Required(ErrorMessage = "Movie name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Release Date is required.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Ticket Price is required.")]
        public decimal TicketPrice { get; set; }
        [Required(ErrorMessage = "Country is required.")]
        public int CountryId { get; set; }
        [MaxFileSize(1 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".gif", ".jpeg" })]
        public required IFormFile Photo { get; set; }
        [Required(ErrorMessage = "Genre is required.")]
        public virtual List<string> MovieGenres { get; set; }
    }
}
