using LinQuisine.Models;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("AllowAll")]
    [Consumes("application/json")]
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

        #region Id CRUD

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

        #endregion

        #region Name CRUD

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

        #endregion
    }
}
