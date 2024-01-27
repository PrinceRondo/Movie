using Movie.Core.Data;
using Movie.Core.Entities.Configuration;
using Movie.Core.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Repository.Implementation.Configuration
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Country?> GetCountryByName(string name)
        {
            return await _context.Countries.Where(x => x.CountryName.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }
    }
}
