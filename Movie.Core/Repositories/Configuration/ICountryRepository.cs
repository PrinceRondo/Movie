using Movie.Core.Entities;
using Movie.Core.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Repositories.Configuration
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task<Country?> GetCountryByName(string name);
    }
}
