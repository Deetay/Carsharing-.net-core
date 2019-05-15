using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class RequestModel
    {
        public IFormFile FileStream { get; set; }

    }
}
