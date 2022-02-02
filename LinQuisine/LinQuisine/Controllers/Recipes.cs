using LinQuisine.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class Recipes : ControllerBase
    {
        [HttpGet]
        [Consumes("application/json")]
        public List<Recipe> Get()
        {
            return new List<Recipe> {

            };
        }

        [HttpPost]
        [Consumes("application/json")]
        public StatusReponse Post()
        {
            return new StatusReponse
            {
                success = true,
                info = "item added"
            };
        }

        #region Id CRUD

        [HttpGet]
        [Consumes("application/json")]
        [Route("id")]
        public Recipe GetById()
        {
            return new Recipe { };
        }

        [HttpPut]
        [Consumes("application/json")]
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
        [Consumes("application/json")]
        [Route("id")]
        public StatusReponse DeleteById()
        {
            return new StatusReponse
            {
                success = true,
                info = "x item(s) are deleted"
            };
        }

        #endregion

        #region Name CRUD

        [HttpGet]
        [Consumes("application/json")]
        [Route("name")]
        public Recipes GetByName()
        {
            return new Recipes { };
        }

        [HttpPut]
        [Consumes("application/json")]
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
        [Consumes("application/json")]
        [Route("name")]
        public StatusReponse DeleteByName()
        {
            return new StatusReponse
            {
                success = true,
                info = "x item(s) are deleted"
            };
        }

        #endregion
    }
}
