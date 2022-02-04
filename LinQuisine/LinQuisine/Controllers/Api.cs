using LinQuisine.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Consumes("application/json")]
    [Route("[controller]")]
    public class Api : ControllerBase
    {
        [HttpGet]
        [Route("status")]
        public IActionResult Status() => Ok(new Status { online = true });

        [HttpGet]
        [Route("version")]
        public IActionResult Version() => Ok(new Models.Version { version = "0.1.0" });
    }
}
