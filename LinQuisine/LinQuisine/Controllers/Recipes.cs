using LinQuisine.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LinQuisine.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    public class Recipes : ControllerBase
    {
        private List<Recipe> recipes;

        #region Data Manager

        private async Task<List<Recipe>> GetRecipes()
        {
            string _json = await System.IO.File.ReadAllTextAsync(path: $"{Directory.GetCurrentDirectory()}/Database/recipes.json");
            return JsonConvert.DeserializeObject<List<Recipe>>(_json);
        }

        private async Task<bool> UpdateRecipes()
        {
            try
            {
                string _json = JsonConvert.SerializeObject(value: recipes);
                await System.IO.File.WriteAllTextAsync(path: $"{Directory.GetCurrentDirectory()}/Database/recipes.json", contents: _json);
                return true;
            }
            
            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return false;
            }
        }

        private async Task UpdateRecipesToCsv()
        {
            //used NewtonSoft json nuget package
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + JsonConvert.SerializeObject(value: recipes) + "}}");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            XmlReader xmlReader = new XmlNodeReader(xml);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var dataTable = dataSet.Tables[1];

            //Datatable to CSV
            var lines = new List<string>();
            string[] columnNames = dataTable.Columns.Cast<DataColumn>()
                                                    .Select(column => column.ColumnName)
                                                    .ToArray();
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var valueLines = dataTable.AsEnumerable()
                                      .Select(row => string.Join(",", row.ItemArray));
            
            lines.AddRange(valueLines);
            await System.IO.File.WriteAllLinesAsync($"{Directory.GetCurrentDirectory()}/Database/recipes.csv", lines);
        }

        #endregion

        #region Main Routes

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          orderby recipe.id ascending
                                                          group recipe by recipe.id;

                foreach(var item in req)
                    return Ok(value: item);

                return Ok(value: req);
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("csv")]
        public async Task<IActionResult> ExportToCsv()
        {
            await ExportToCsv();

            return Ok(value: new Reponse { success = true, info = "data successfully converted" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Recipe body)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          orderby recipe.id ascending
                                                          group recipe by recipe.id;

                foreach (var item in req)
                    foreach (Recipe recipe in item)
                        if (body.name == recipe.name)
                            return Unauthorized(value: new Reponse { success = false, error = "recipe name already exist" });

                DateTime foo = DateTime.Now;

                recipes.Add(new Recipe(
                    id: (int)((DateTimeOffset)foo).ToUnixTimeSeconds(),
                    userId: profile.id,
                    name: body.name,
                    nbParts: body.nbParts,
                    ingredients: body.ingredients,
                    steps: body.steps
                ));

                await UpdateRecipes();

                return Ok(value: new Reponse { success = true, info = "item added" });
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion

        #region Id Routes

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          where recipe.id.Equals(id)
                                                          group recipe by recipe.id;

                foreach (var item in req)
                    foreach (Recipe recipe in item)
                        return Ok(value: recipe);

                return Ok(value: req);
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("id/{id}")]
        public async Task<IActionResult> PutById(int id, [FromBody] Recipe body)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          where recipe.id.Equals(id)
                                                          group recipe by recipe.id;

                foreach (var item in req)
                {

                    foreach (Recipe recipe in item)
                    {
                        recipe.name = body.name;
                        recipe.nbParts = body.nbParts;
                        recipe.ingredients = body.ingredients;
                        recipe.steps = body.steps;
                    }
                }

                await UpdateRecipes();

                return Ok(value: new Reponse { success = true, info = "item is updated" });
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("id/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          where recipe.id.Equals(id)
                                                          group recipe by recipe.id;

                return Ok(value: new Reponse { success = true, info = "item is deleted" });
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        #endregion

        #region Name Routes

        [HttpGet]
        [Route("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          where recipe.name.Contains(name)
                                                          group recipe by recipe.id;

                foreach (var item in req)
                    foreach (Recipe recipe in item)
                        return Ok(value: recipe);

                return Ok(value: req);
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("name/{name}")]
        public async Task<IActionResult> PutByName(string name, [FromBody] Recipe body)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          where recipe.name.Contains(name)
                                                          group recipe by recipe.id;

                foreach (var item in req)
                {

                    foreach (Recipe recipe in item)
                    {
                        recipe.name = body.name;
                        recipe.nbParts = body.nbParts;
                        recipe.ingredients = body.ingredients;
                        recipe.steps = body.steps;
                    }
                }

                await UpdateRecipes();

                return Ok(value: new Reponse { success = true, info = "item is updated" });
            }

            catch (InvalidCastException e)
            {
                Console.WriteLine(value: e);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("name/{name}")]
        public async Task<IActionResult> DeleteByName(string name)
        {
            var headers = Request.Headers;

            try
            {
                string token = headers["authorization"];
                Profile profile = JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(Convert.FromBase64String(token)));

                recipes = await GetRecipes();

                IEnumerable<IGrouping<int, Recipe>> req = from recipe in recipes
                                                          where recipe.userId.Equals(profile.id)
                                                          where recipe.name.Contains(name)
                                                          group recipe by recipe.id;

                return Ok(value: new Reponse { success = true, info = "item is deleted" });
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
