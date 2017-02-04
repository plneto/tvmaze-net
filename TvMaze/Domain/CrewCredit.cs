using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMaze.Domain {
    public class CrewCredit {
        public string Type { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public IDictionary<LinkType, Link> Links { get; set; }

        public Show Show { get; set; }
    }
}
