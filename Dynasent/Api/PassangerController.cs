using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dynasent.Api
{
    [Route("api/v1/passanger")]
    public class PassangerController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            return Ok(); 
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<bool> PostNewPassanger([FromBody]string value)
        {
            return true;
        }

        // PUT api/<controller>/5
        [HttpPut("{busId}/{passangerId}/{passangerName}")]
        public void Put(int busId, int passangerId, string passangerName )
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{deleteToken}/{id}")]
        public void Delete(string deleteToken, int id)
        {
            string tokenType = "Passanger_Token";
        }
    }
}
