﻿using Movie.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Entities.Configuration
{
    public class Country : BaseEntity
    {
        public string? CountryName { get; set; }
    }
}
