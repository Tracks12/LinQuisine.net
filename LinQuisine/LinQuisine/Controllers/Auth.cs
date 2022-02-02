using LinQuisine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinQuisine.Models.Reponse;

namespace LinQuisine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        private IConfiguration _config;

        private readonly List<User> users = new() {
            //new User(id: 0, username: "admin", mail: "admin@localhost", password: "admin", token: ""),
            //new User(id: 1, username: "user", mail: "user@localhost", password: "pass", token: ""),
            //new User(id: 2, username: "guest", mail: "guest@localhost", password: "guest", token: "")
        };

        #region Register

        [HttpPost]
        [Consumes("application/json")]
        [Route("register")]
        public async Task<IActionResult> Register(Register body)
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
        public async Task<IActionResult> Login(Login body)
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

                return NotFound(new StatusReponse { success = false, info = "user not found" });
            }
            
            catch(InvalidCastException)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Logout

        [HttpPost]
        [Consumes("application/json")]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                return Ok(new StatusReponse { success = true, info = "user successfully disconnected" });
            }

            catch(InvalidCastException)
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
