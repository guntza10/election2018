using System;

namespace ElectionMana.Api.Models
{
    public class PoliticalParty
    {
        public string id { get; set; }
        public string name { get; set; }
        public int haveScore { get; set; }
        public int areaScore { get; set; }
        public int proportionScore { get; set; }
    }
}