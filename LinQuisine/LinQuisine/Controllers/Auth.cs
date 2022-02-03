using LinQuisine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static LinQuisine.Models.Reponse;

namespace LinQuisine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        private readonly List<User> users = new() {
            new User(id: 0, username: "admin", mail: "admin@localhost", password: "admin", token: ""),
            new User(id: 1, username: "user", mail: "user@localhost", password: "pass", token: ""),
            new User(id: 2, username: "guest", mail: "guest@localhost", password: "guest", token: "")
        };

        #region Register

        [HttpPost]
        [Consumes("application/json")]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register body)
        {
            try
            {
                foreach (User user in users)
                {
                    if (body.username == user.username || body.mail == user.mail)
                        return Unauthorized(value: new StatusReponse { success = false, error = "user already exist" });
                }

                users.Add(new User(id: users.Count, username: body.username, mail: body.mail, password: body.password, token: ""));

                return Ok(value: new StatusReponse { success = false, info = "user successfully registered" });
            }
            
            catch(InvalidCastException)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Login

        [HttpPost]
        [Consumes("application/json")]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login body)
        {
            try
            {
                foreach (User user in users)
                {
                    if(body.username == user.username && body.password == user.password)
                    {
                        return Ok(new Profile { id = user.id, username = user.username, mail = user.mail, token = user.token });
                    }
                }

                return NotFound(new StatusReponse { success = false, error = "user not found" });
            }
            
            catch(InvalidCastException)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Logout

        [HttpPost("GetAllHeaders")]
        [Consumes("application/json")]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var headers = Request.Headers;

                if (headers.ContainsKey("authorization"))
                {
                    Console.WriteLine(headers["authorization"]);
                    return Ok(new StatusReponse { success = true, info = "user successfully disconnected" });
                }

                else
                {
                    return BadRequest();
                }
            }

            catch(InvalidCastException)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
