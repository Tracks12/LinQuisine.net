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
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        [Route("register")]
        public Profile Register()
        {
            return new Profile
            {
                id = 0,
                username = "admin",
                mail = "admin@localhost"
            };
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("login")]
        public Profile Login()
        {
            return new Profile
            {
                id = 0,
                username = "admin",
                mail = "admin@localhost"
            };
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("logout")]
        public Logout Logout()
        {
            return new Logout { success = true};
        }
    }
}
