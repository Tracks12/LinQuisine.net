using LinQuisine.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    [Route("api/[controller]")]
    public class Recipes : ControllerBase
    {
        private List<Recipe> recipes = new()
        {
            new Recipe(id: 1, userId: 1, name: "Mousse express au Nutella", nbParts: 6, ingredients: new List<Ingredient>() { new Ingredient { }, new Ingredient { } }, steps: new List<string>() { "", "" }),
            new Recipe(id: 2, userId: 1, name: "Tarte au chèvre et saumon", nbParts: 2, ingredients: new List<Ingredient>() { new Ingredient { } }, steps: new List<string>() { "", "", "" }),
            new Recipe(id: 3, userId: 1, name: "Poop pie", nbParts: 4, ingredients: new List<Ingredient>() { new Ingredient { } }, steps: new List<string>() { "Tous manger" }),
            new Recipe(id: 4, userId: 2, name: "random recipe", nbParts: 8, ingredients: new List<Ingredient>() { new Ingredient { } }, steps: new List<string>() { "", "" })
        };

        private async Task<List<Recipe>> GetRecipes()
        {
            string _json = await System.IO.File.ReadAllTextAsync("Database/recipes.json");
            return JsonConvert.DeserializeObject<List<Recipe>>(_json);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(value: recipes);
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok(value: new Reponse { success = true, info = "item added" });
        }

        #region Id CRUD

        [HttpGet]
        [Route("id")]
        public IActionResult GetById()
        {
            return Ok(value: recipes[1]);
        }

        [HttpPut]
        [Route("id")]
        public IActionResult PutById()
        {
            return Ok(value: new Reponse { success = true, info = "x item(s) are updated" });
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteById()
        {
            return Ok(value: new Reponse { success = true, info = "x item(s) are deleted" });
        }

        #endregion

        #region Name CRUD

        [HttpGet]
        [Route("name")]
        public IActionResult GetByName()
        {
            return Ok(value: recipes[1]);
        }

        [HttpPut]
        [Route("name")]
        public IActionResult PutByName()
        {
            return Ok(value: new Reponse { success = true, info = "x item(s) are updated" });
        }

        [HttpDelete]
        [Route("name")]
        public IActionResult DeleteByName()
        {
            return Ok(value: new Reponse { success = true, info = "x item(s) are deleted" });
        }

        #endregion
    }
}
