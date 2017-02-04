using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMaze.Domain {
    public class Person {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public IDictionary<ImageType, string>  Image { get; set; }
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<LinkType, Link> Links { get; set; }
        public IList<CastCredit>  CastCredits { get; set; }
    }
}
