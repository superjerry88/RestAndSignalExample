using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAS.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RestAndSignalExample.Hubs;

namespace RestAndSignalExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyController : ControllerBase
    {

        // GET: api/Key
        [HttpGet]
        public string Get()
        {
            return $"Total Clients: {ManagerHub.ConnectedIds.Count}";
        }

        // GET: api/Key/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            ManagerHub.Send("Update",id);
            return $"Total of:{ManagerHub.ConnectedIds.Count} clients received: [{id}]";
        }

        // POST: api/Key
        [HttpPost]
        public string Post([FromBody] Asset asset)
        {
            return $"Received {asset.Name} - {asset.Irai}";
        }

        // PUT: api/Key/5
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

class PurchasedOrder
{
    private DateTime ExpiryDate;
    private string ProviderOrgId;
    private string ConsumerOrgId;
    private string IRAI;
    private string HostUrl;
    private string SecretKey;
}
