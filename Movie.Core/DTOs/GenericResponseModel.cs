﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.DTOs
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class GenericResponseModel
    {
        public long StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public TokenClass? TokenClass { get; set; }
        public object? Data { get; set; }
    }
    public class TokenClass
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
