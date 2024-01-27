using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.DTOs.RequestDtos.Configuration
{
    public record CountryDto
    {
        public string? CountryName { get; set; }
    }
}
