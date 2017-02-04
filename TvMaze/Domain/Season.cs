using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TvMaze.Domain {
    public class Season {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public ushort EpisodeOrder { get; set; }
        public DateTime PremiereDate { get; set; }
        public DateTime EndDate { get; set; }
        public Network Network { get; set; }
        public WebChannel WebChannel { get; set; }
        public IDictionary<ImageType, string> Image { get; set; }
        public string Summary { get; set; }
        [JsonProperty(PropertyName = "_links")]
        public IDictionary<LinkType, Link> Links { get; set; }
    }
}
