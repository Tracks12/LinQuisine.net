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
        private List<User> users;

        public Auth() {}

        #region Data Manager

        private async Task<List<User>> GetUsers()
        {
            string _json = await System.IO.File.ReadAllTextAsync(path: $"{Directory.GetCurrentDirectory()}/Database/users.json");
            return JsonConvert.DeserializeObject<List<User>>(_json);
        }

        private async Task<bool> UpdateUsers()
        {
            try
            {
                string _json = JsonConvert.SerializeObject(value: users);
                await System.IO.File.WriteAllTextAsync(path: $"{Directory.GetCurrentDirectory()}/Database/users.json", contents: _json);
                return true;
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return false;
            }
        }

        #endregion

        #region Register

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] Register body)
        {
            try
            {
                users = await GetUsers();

                IEnumerable<IGrouping<int, User>> req = from user in users
                                                        orderby user.id ascending
                                                        group user by user.id;

                foreach (var item in req)
                    foreach (User user in item)
                        if (body.username == user.username || body.mail == user.mail)
                            return Unauthorized(value: new Reponse { success = false, error = "user already exist" });

                DateTime foo = DateTime.Now;

                users.Add(new User(id: (int)((DateTimeOffset)foo).ToUnixTimeSeconds(), username: body.username, mail: body.mail, password: body.password, token: null));

                await UpdateUsers();

                return Ok(value: new Reponse { success = true, info = "user successfully registered" });
            }
            
            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion

        #region Login

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login body)
        {
            try
            {
                users = await GetUsers();

                IEnumerable<IGrouping<int, User>> req = from user in users
                                                        where user.username.Contains(body.username)
                                                        where user.password.Contains(body.password)
                                                        group user by user.id;

                foreach (var item in req)
                {
                    foreach (User user in item)
                    {
                        Profile profile = new Profile(id: user.id, username: user.username, mail: user.mail);
                        string token = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(profile)));

                        user.token = token;

                        await UpdateUsers();

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

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                users = await GetUsers();

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
                            user.token = null;

                    await UpdateUsers();

                    return Ok(value: new Reponse { success = true, info = "user successfully disconnected" });
                }

                else
                {
                    return BadRequest();
                }
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion
    }
}
