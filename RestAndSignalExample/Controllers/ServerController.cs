using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAndSignalExample.Hubs;

namespace RestAndSignalExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        // GET: api/Server
        [HttpGet]
        public int Get()
        {
            return ManagerHub.ConnectedIds.Count;
        }

        // GET: api/Server/5
        [HttpGet("{id}")]
        public string Get(string query)
        {
            switch (query)
            {
                
            }
            return "value";
        }

        // POST: api/Server
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Server/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
