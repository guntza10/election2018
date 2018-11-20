using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ElectionMana.Api.Models;
using MongoDB.Driver;
using System.Security.Authentication;

namespace ElectionMana.Api.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("api/[controller]/[action]/")]
    public class PoliticalController : Controller
    {
        IMongoCollection<PoliticalParty> PoliticalPartyCollection { get; set; }
        public PoliticalController()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl("mongodb://admin:abcd1234@ds143532.mlab.com:43532/electionmana"));
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase("electionmana");
            PoliticalPartyCollection = database.GetCollection<PoliticalParty>("PoliticalParty");
        }

        [HttpGet]
        public IEnumerable<PoliticalParty> GetAll() {
            var politicalParty = PoliticalPartyCollection.Find(it => true).ToList();
            return politicalParty;
        }

        [HttpPost]
        public void CreatePoliticalParty([FromBody]PoliticalParty newPoliticalParty) {
            newPoliticalParty.id = Guid.NewGuid().ToString();
            PoliticalPartyCollection.InsertOne(newPoliticalParty);
        }

        [HttpPost]
        public void EditPoliticalParty([FromBody]PoliticalParty editPoliticalParty) {
            PoliticalPartyCollection.ReplaceOne(it => it.id == editPoliticalParty.id,editPoliticalParty);
        }
    }
}
