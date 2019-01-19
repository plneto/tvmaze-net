using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMaze.Domain {
    public class Person {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public Gender? Gender { get; set; }
        public IDictionary<ImageType, string>  Image { get; set; }
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<LinkType, Link> Links { get; set; }
        public IList<CastCredit>  CastCredits { get; set; }
    }
}
