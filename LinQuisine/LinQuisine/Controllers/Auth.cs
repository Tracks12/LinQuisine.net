using LinQuisine.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQuisine.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        private List<User> users = new() {
            new User(id: 0, username: "admin", mail: "admin@localhost", password: "admin", token: ""),
            new User(id: 1, username: "user", mail: "user@localhost", password: "pass", token: ""),
            new User(id: 2, username: "guest", mail: "guest@localhost", password: "guest", token: "")
        };

        private async Task<List<User>> GetUsers()
        {
            string _json = await System.IO.File.ReadAllTextAsync($"{Directory.GetParent(Directory.GetCurrentDirectory())}Database/users.json");
            return JsonConvert.DeserializeObject<List<User>>(_json);
        }

        #region Register

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register body)
        {
            try
            {
                foreach (User user in users)
                {
                    if (body.username == user.username || body.mail == user.mail)
                        return Unauthorized(value: new Reponse { success = false, error = "user already exist" });
                }

                users.Add(new User(id: users.Count, username: body.username, mail: body.mail, password: body.password, token: ""));

                return Ok(value: new Reponse { success = false, info = "user successfully registered" });
            }
            
            catch(InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion

        #region Login

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Login body)
        {
            try
            {
                IEnumerable<IGrouping<int, User>> req = from user in users
                                                        where user.username.Contains(body.username)
                                                        where user.password.Contains(body.password)
                                                        group user by user.id;

                foreach (var item in req)
                {
                    foreach (User user in users)
                    {
                        Profile profile = new Profile(id: user.id, username: user.username, mail: user.mail);
                        string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(profile)));

                        user.token = token;

                        return Ok(value: new Connection(profile: profile, token: token));
                    }
                }

                return NotFound(value: new Reponse { success = false, error = "user not found" });
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion

        #region Logout

        [HttpPost("GetAllHeaders")]
        [Route("logout")]
        public IActionResult Logout()
        {
            try
            {
                var headers = Request.Headers;

                if (headers.ContainsKey("authorization"))
                {
                    string token = headers["authorization"];
                    Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                    IEnumerable<IGrouping<int, User>> req = from user in users
                                                            where user.id.Equals(profile.id)
                                                            group user by user.id;

                    foreach (var item in req)
                        foreach (User user in item)
                            user.token = "";

                    return Ok(value: new Reponse { success = true, info = "user successfully disconnected" });
                }

                else
                {
                    return BadRequest();
                }
            }

            catch(InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion
    }
}
