using LinQuisine.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Api : ControllerBase
    {
        [HttpGet]
        [Consumes("application/json")]
        [Route("status")]
        public Status Status()
        {
            return new Status { online = true };
        }

        [HttpGet]
        [Consumes("application/json")]
        [Route("version")]
        public Models.Version Version()
        {
            return new Models.Version { version = "0.1.0" };
        }
    }
}
