using Movie.Core.Data;
using Movie.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Entities.Movie
{
    public class MovieGenre : BaseEntity
    {
        public string? Genre { get; set; }
        public int MovieId { get; set; }
        

        [ForeignKey("MovieId")]
        public virtual Movies? Movies { get; set; }
    }
}
