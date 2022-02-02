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
    public class Recipes : ControllerBase
    {
        [HttpGet]
        public List<Recipe> Get()
        {
            return new List<Recipe> {

            };
        }

        [HttpPost]
        public StatusReponse Post()
        {
            return new StatusReponse
            {
                success = true,
                info = "item added"
            };
        }

        [HttpGet]
        [Route("id")]
        public Recipe GetById()
        {
            return new Recipe { };
        }

        [HttpPut]
        [Route("id")]
        public StatusReponse PutById()
        {
            return new StatusReponse
            {
                success = true,
                info = "x item(s) are updated"
            };
        }

        [HttpDelete]
        [Route("id")]
        public StatusReponse DeleteById()
        {
            return new StatusReponse
            {
                success = true,
                info = "x item(s) are deleted"
            };
        }

        [HttpGet]
        [Route("name")]
        public Recipes GetByName()
        {
            return new Recipes { };
        }

        [HttpPut]
        [Route("name")]
        public StatusReponse PutByName()
        {
            return new StatusReponse
            {
                success = true,
                info = "x item(s) are updated"
            };
        }

        [HttpDelete]
        [Route("name")]
        public StatusReponse DeleteByName()
        {
            return new StatusReponse
            {
                success = true,
                info = "x item(s) are deleted"
            };
        }
    }
}
