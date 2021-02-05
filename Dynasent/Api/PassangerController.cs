using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dynasent.Services;
using Dynasent.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dynasent.Api
{
    [Route("api/v1/passanger/")]
    [ApiController]
    [Produces("application/json")]
    public class PassangerController : Controller
    {
        private readonly PassangerService _PassangerMan;

        public PassangerController(PassangerService passangerService)
        {
            this._PassangerMan = passangerService;
        }
        // GET: api/<controller>
        [HttpGet("{busId}")]
        public async Task<ActionResult<List<PassangerViewModel>>> GetAllData(int busId)
        {
            var allPassanger = await _PassangerMan.GetDataPassanger();
            var specifiedPassanger = allPassanger.Where(Q => Q.BusId == busId)
                .Select(Q => Q).ToList();
            return specifiedPassanger; 
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult> PostNewPassanger(string jsonData)
        {
            var passanger = JsonConvert.DeserializeObject<PassangerViewModel>(jsonData);
            await _PassangerMan.InsertPassanger(passanger);

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{busId}/{passangerId}/{passangerName}")]
        public async Task<ActionResult> PutAsync(int busId, int passangerId, string passangerName)
        {
            await _PassangerMan.InOrOutPassanger(passangerId, busId, passangerName);
            Response.Headers.Add("Refresh", "5");
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{deleteToken}/{id}")]
        public void Delete(string deleteToken, int id)
        {
            //string tokenType = "Passanger_Token";
        }
    }
}
